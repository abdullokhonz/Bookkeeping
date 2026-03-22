using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookCategoryDto;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookDto;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.ReferenceBooks
{
    public partial class AllReferenceBooks
    {
        private bool _isLoading = true;
        private List<TreeNode> _treeNodes = new();
        private HashSet<Guid> _expandedNodes = new();
        private Dictionary<Guid, string> _accountCache = new();

        public enum NodeType { Category, ReferenceBook }

        public class TreeNode
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = "";
            public string? Description { get; set; }
            public NodeType Type { get; set; }
            public Guid? AccountId { get; set; }
            public int InfoItemsCount { get; set; } = 0;
            public List<TreeNode> Children { get; set; } = new();
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            _isLoading = true;
            try
            {
                var categoriesTask = Http.GetFromJsonAsync<ApiResponse<List<ReferenceBookCategoryGetDto>>>("/api/v1/ReferenceBookCategory/GetAll");
                var booksTask = Http.GetFromJsonAsync<ApiResponse<List<ReferenceBookGetDto>>>("/api/v1/ReferenceBook/GetAll");

                await Task.WhenAll(categoriesTask, booksTask);

                var categoriesResponse = await categoriesTask;
                var booksResponse = await booksTask;

                if (categoriesResponse?.IsSuccess == true && booksResponse?.IsSuccess == true)
                {
                    _treeNodes = BuildTree(categoriesResponse.Data ?? new(), booksResponse.Data ?? new());

                    _ = FetchMissingAccountNames();
                }
                else
                {
                    Snackbar.Add("Ошибка загрузки данных", Severity.Error);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Ошибка: {ex.Message}", Severity.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }

        private List<TreeNode> BuildTree(List<ReferenceBookCategoryGetDto> categories, List<ReferenceBookGetDto> books)
        {
            var rootNodes = new List<TreeNode>();
            var categoryNodesMap = new Dictionary<Guid, TreeNode>();

            foreach (var cat in categories)
            {
                var catNode = new TreeNode
                {
                    Id = cat.Id,
                    Name = cat.Name,
                    Description = cat.Description,
                    Type = NodeType.Category,
                    AccountId = cat.IfrsAccountId
                };
                categoryNodesMap[cat.Id] = catNode;
                rootNodes.Add(catNode);
            }

            foreach (var book in books)
            {
                var bookNode = new TreeNode
                {
                    Id = book.Id,
                    Name = book.Name,
                    Description = book.Description,
                    Type = NodeType.ReferenceBook,
                    InfoItemsCount = book.Info?.Count ?? 0,
                    AccountId = book.SubIfrsAccountId
                };

                if (categoryNodesMap.TryGetValue(book.ReferenceBookCategoryId, out var parentCategory))
                {
                    parentCategory.Children.Add(bookNode);
                }
                else
                {
                    rootNodes.Add(bookNode);
                }
            }

            return rootNodes;
        }

        private async Task FetchMissingAccountNames()
        {
            var allAccountIds = GetAllAccountIds(_treeNodes)
                .Where(id => id != Guid.Empty && !_accountCache.ContainsKey(id))
                .Distinct();

            foreach (var id in allAccountIds)
            {
                try
                {
                    var res = await Http.GetFromJsonAsync<ApiResponse<IfrsAccountTreeDto>>($"/api/v1/IfrsAccount/GetById/{id}");
                    if (res != null && res.IsSuccess && res.Data != null)
                    {
                        _accountCache[id] = res.Data.AccountNumber;
                        StateHasChanged();
                    }
                }
                catch { }
            }
        }

        private IEnumerable<Guid> GetAllAccountIds(List<TreeNode> nodes)
        {
            foreach (var node in nodes)
            {
                if (node.AccountId.HasValue) yield return node.AccountId.Value;
                foreach (var childId in GetAllAccountIds(node.Children)) yield return childId;
            }
        }

        private void ToggleNode(Guid id)
        {
            if (_expandedNodes.Contains(id)) _expandedNodes.Remove(id);
            else _expandedNodes.Add(id);
        }

        private void NavigateToDetail(TreeNode node)
        {
            if (node.Type == NodeType.ReferenceBook)
                Nav.NavigateTo($"/reference-books/details/{node.Id}");
            else
                Nav.NavigateTo($"/reference-books/categories/details/{node.Id}");
        }
    }
}
