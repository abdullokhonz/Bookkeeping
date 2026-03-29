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
        private List<TreeNode> _allTreeNodes = new();
        private List<TreeNode> _displayTreeNodes = new();
        private HashSet<Guid> _expandedNodes = new();
        private Dictionary<Guid, string> _accountCache = new();

        private string _searchString = "";
        private string _searchField = "All";
        private string _sortBy = "Account";
        private SortDirection _sortDirection = SortDirection.Ascending;

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
                    _allTreeNodes = BuildTree(categoriesResponse.Data ?? new(), booksResponse.Data ?? new());
                    _ = FetchMissingAccountNames();
                    ApplyFilterAndSort();
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
            var categoryNodesMap = categories.ToDictionary(c => c.Id, c => new TreeNode
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Type = NodeType.Category,
                AccountId = c.IfrsAccountId
            });

            var rootNodes = categoryNodesMap.Values.ToList();

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
                    rootNodes.Remove(bookNode);
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
            var allAccountIds = GetAllAccountIds(_allTreeNodes)
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
                        ApplyFilterAndSort();
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

        private void OnSearchStringChanged(string s) { _searchString = s; ApplyFilterAndSort(); }
        private void OnSearchFieldChanged(string f) { _searchField = f; ApplyFilterAndSort(); }
        private void ResetSearch() { _searchString = ""; _searchField = "All"; ApplyFilterAndSort(); }

        private void ToggleSort(string field)
        {
            if (_sortBy == field)
            {
                if (_sortDirection == SortDirection.Ascending)
                    _sortDirection = SortDirection.Descending;
                else if (_sortDirection == SortDirection.Descending)
                    _sortDirection = SortDirection.None;
                else
                    _sortDirection = SortDirection.Ascending;
            }
            else
            {
                _sortBy = field;
                _sortDirection = SortDirection.Ascending;
            }
            ApplyFilterAndSort();
        }

        private string GetSortIconClass(string field)
        {
            if (_sortBy != field || _sortDirection == SortDirection.None)
                return "custom-sort-icon";
            var baseClass = "custom-sort-icon active";
            return _sortDirection == SortDirection.Descending ? $"{baseClass} desc" : baseClass;
        }

        private void ApplyFilterAndSort()
        {
            var filtered = FilterTree(_allTreeNodes);
            SortTree(filtered);
            _displayTreeNodes = filtered;
            StateHasChanged();
        }

        private List<TreeNode> FilterTree(List<TreeNode> nodes)
        {
            var result = new List<TreeNode>();
            bool isSearchEmpty = string.IsNullOrWhiteSpace(_searchString);

            foreach (var node in nodes)
            {
                var filteredChildren = FilterTree(node.Children);
                bool matches = MatchesSearch(node);

                if (isSearchEmpty || matches || filteredChildren.Any())
                {
                    var clone = CloneNode(node);
                    clone.Children = filteredChildren;
                    if (!isSearchEmpty && filteredChildren.Any()) _expandedNodes.Add(node.Id);
                    result.Add(clone);
                }
            }
            return result;
        }

        private bool MatchesSearch(TreeNode node)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            var s = _searchString.ToLower();
            _accountCache.TryGetValue(node.AccountId ?? Guid.Empty, out var acc);
            acc = acc?.ToLower() ?? "";

            return _searchField switch
            {
                "Name" => node.Name.ToLower().Contains(s),
                "Description" => (node.Description ?? "").ToLower().Contains(s),
                "Account" => acc.Contains(s),
                _ => node.Name.ToLower().Contains(s) || (node.Description ?? "").ToLower().Contains(s) || acc.Contains(s)
            };
        }

        private void SortTree(List<TreeNode> nodes)
        {
            if (_sortDirection != SortDirection.None)
            {
                nodes.Sort((a, b) =>
                {
                    var res = Comparer<object>.Default.Compare(GetSortValue(a), GetSortValue(b));
                    return _sortDirection == SortDirection.Ascending ? res : -res;
                });
            }
            foreach (var node in nodes) SortTree(node.Children);
        }

        private object GetSortValue(TreeNode node) => _sortBy switch
        {
            "Name" => node.Name,
            "Description" => node.Description ?? "",
            "Account" => _accountCache.TryGetValue(node.AccountId ?? Guid.Empty, out var acc) ? acc : "999999",
            _ => node.Name
        };

        private TreeNode CloneNode(TreeNode n) => new TreeNode
        {
            Id = n.Id,
            Name = n.Name,
            Description = n.Description,
            Type = n.Type,
            AccountId = n.AccountId,
            InfoItemsCount = n.InfoItemsCount
        };

        private void ToggleNode(Guid id)
        {
            if (_expandedNodes.Contains(id)) _expandedNodes.Remove(id);
            else _expandedNodes.Add(id);
        }

        private void NavigateToDetail(TreeNode node)
        {
            Nav.NavigateTo(node.Type == NodeType.ReferenceBook
                ? $"/reference-books/details/{node.Id}"
                : $"/reference-books/categories/details/{node.Id}");
        }
    }
}
