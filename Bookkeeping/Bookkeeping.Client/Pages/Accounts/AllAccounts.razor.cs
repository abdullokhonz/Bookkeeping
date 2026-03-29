using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.Accounts5d.CategoryAccount5dDto;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Contracts.Enums;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.Accounts
{
    public partial class AllAccounts
    {
        private bool _isLoading = true;

        private List<TreeNode> _originalTreeNodes = new();

        private List<TreeNode> _displayTreeNodes = new();

        private HashSet<Guid> _expandedNodes = new();

        private string _searchString = "";
        private string _searchField = "All";

        private string _sortBy = "AccountNumber";
        private SortDirection _sortDirection = SortDirection.Ascending;

        public class TreeNode
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = "";
            public string? Description { get; set; }
            public string? AccountNumber { get; set; }
            public AccountNature? Nature { get; set; }
            public NodeType Type { get; set; }
            public List<TreeNode> Children { get; set; } = new();

            public TreeNode Clone() => new TreeNode
            {
                Id = Id,
                Name = Name,
                Description = Description,
                AccountNumber = AccountNumber,
                Nature = Nature,
                Type = Type,
                Children = new List<TreeNode>()
            };
        }

        public enum NodeType { Category, Account }

        protected override async Task OnInitializedAsync() => await LoadDataAsync();

        private async Task LoadDataAsync()
        {
            _isLoading = true;
            try
            {
                var categoriesTask = Http.GetFromJsonAsync<ApiResponse<List<CategoryAccount5dTreeDto>>>("/api/v1/CategoryAccount5d/tree");
                var accountsTask = Http.GetFromJsonAsync<ApiResponse<List<IfrsAccountTreeDto>>>("/api/v1/IfrsAccount/tree");

                await Task.WhenAll(categoriesTask, accountsTask);

                var catResponse = categoriesTask.Result;
                var accResponse = accountsTask.Result;

                if (catResponse?.IsSuccess == true && accResponse?.IsSuccess == true)
                {
                    _originalTreeNodes = BuildTree(catResponse.Data ?? new(), accResponse.Data ?? new());
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

        private List<TreeNode> BuildTree(List<CategoryAccount5dTreeDto> categories, List<IfrsAccountTreeDto> accounts)
        {
            var categoryNodes = new Dictionary<Guid, TreeNode>();
            var rootCategories = new List<TreeNode>();

            void AddCategoryNode(CategoryAccount5dTreeDto cat, TreeNode? parentNode = null)
            {
                var node = new TreeNode
                {
                    Id = cat.Id,
                    Name = cat.Name,
                    Description = cat.Description,
                    Type = NodeType.Category
                };
                categoryNodes[cat.Id] = node;

                if (parentNode == null) rootCategories.Add(node);
                else parentNode.Children.Add(node);

                foreach (var child in cat.Children ?? new()) AddCategoryNode(child, node);
            }

            foreach (var cat in categories) AddCategoryNode(cat);

            TreeNode ConvertAccount(IfrsAccountTreeDto acc)
            {
                var node = new TreeNode
                {
                    Id = acc.Id,
                    Name = acc.AccountName,
                    AccountNumber = acc.AccountNumber,
                    Nature = acc.AccountNature,
                    Type = NodeType.Account
                };
                foreach (var child in acc.Children ?? new()) node.Children.Add(ConvertAccount(child));
                return node;
            }

            foreach (var acc in accounts)
            {
                var accountNode = ConvertAccount(acc);
                if (acc.CategoryAccountId != Guid.Empty && categoryNodes.TryGetValue(acc.CategoryAccountId, out var catNode))
                    catNode.Children.Add(accountNode);
                else
                    rootCategories.Add(accountNode);
            }

            return rootCategories;
        }

        private void OnSearchStringChanged(string value)
        {
            _searchString = value;
            ApplyFilterAndSort();
        }

        private void OnSearchFieldChanged(string value)
        {
            _searchField = value;
            ApplyFilterAndSort();
        }

        private void ResetSearch()
        {
            _searchString = string.Empty;
            _searchField = "All";
            ApplyFilterAndSort();
        }

        private string GetSortIconClass(string field)
        {
            if (_sortBy != field || _sortDirection == SortDirection.None)
                return "custom-sort-icon";

            var baseClass = "custom-sort-icon active";
            return _sortDirection == SortDirection.Descending ? $"{baseClass} desc" : baseClass;
        }

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

        private void ApplyFilterAndSort()
        {
            var filtered = FilterNodes(_originalTreeNodes);
            SortNodes(filtered);
            _displayTreeNodes = filtered;
        }

        private List<TreeNode> FilterNodes(IEnumerable<TreeNode> nodes)
        {
            var result = new List<TreeNode>();
            var search = _searchString.Trim().ToLower();

            foreach (var node in nodes)
            {
                bool matchesSelf = string.IsNullOrWhiteSpace(search) || MatchesSearch(node, search);
                var filteredChildren = FilterNodes(node.Children);

                if (matchesSelf || filteredChildren.Any())
                {
                    var clonedNode = node.Clone();
                    clonedNode.Children = filteredChildren;

                    if (!string.IsNullOrWhiteSpace(search) && filteredChildren.Any())
                    {
                        _expandedNodes.Add(clonedNode.Id);
                    }

                    result.Add(clonedNode);
                }
            }
            return result;
        }

        private bool MatchesSearch(TreeNode node, string search)
        {
            string natureStr = node.Nature.HasValue ? GetNatureName(node.Nature).ToLower() : "";
            string accNum = (node.AccountNumber ?? "").ToLower();
            string name = (node.Name ?? "").ToLower();

            return _searchField switch
            {
                "AccountNumber" => accNum.Contains(search),
                "Name" => name.Contains(search),
                "Type" => natureStr.Contains(search),
                _ => accNum.Contains(search) || name.Contains(search) || natureStr.Contains(search)
            };
        }

        private void SortNodes(List<TreeNode> nodes)
        {
            if (_sortDirection != SortDirection.None)
            {
                bool isAsc = _sortDirection == SortDirection.Ascending;

                if (_sortBy == "AccountNumber")
                {
                    nodes.Sort((a, b) => isAsc
                        ? ParseNumberForSorting(a.AccountNumber).CompareTo(ParseNumberForSorting(b.AccountNumber))
                        : ParseNumberForSorting(b.AccountNumber).CompareTo(ParseNumberForSorting(a.AccountNumber)));
                }
                else if (_sortBy == "Name")
                {
                    nodes.Sort((a, b) => isAsc
                        ? string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase)
                        : string.Compare(b.Name, a.Name, StringComparison.OrdinalIgnoreCase));
                }
                else if (_sortBy == "Type")
                {
                    nodes.Sort((a, b) => isAsc
                        ? string.Compare(GetNatureName(a.Nature), GetNatureName(b.Nature), StringComparison.OrdinalIgnoreCase)
                        : string.Compare(GetNatureName(b.Nature), GetNatureName(a.Nature), StringComparison.OrdinalIgnoreCase));
                }
            }

            foreach (var node in nodes)
            {
                if (node.Children.Any())
                {
                    SortNodes(node.Children);
                }
            }
        }

        private double ParseNumberForSorting(string? accountStr)
        {
            if (string.IsNullOrWhiteSpace(accountStr)) return 0;
            if (double.TryParse(accountStr.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var result))
                return result;
            return 0;
        }

        private void ToggleExpand(Guid id)
        {
            if (_expandedNodes.Contains(id)) _expandedNodes.Remove(id);
            else _expandedNodes.Add(id);
        }

        private void NavigateToDetail(TreeNode node)
        {
            if (node.Type == NodeType.Account) NavManager.NavigateTo($"/accounts/details/{node.Id}");
            else NavManager.NavigateTo($"/accounts/categories/details/{node.Id}");
        }

        private string GetNatureName(AccountNature? nature) => nature switch
        {
            AccountNature.Active => "Активный",
            AccountNature.Passive => "Пассивный",
            AccountNature.ActivePassive => "Активно-пассивный",
            _ => nature?.ToString() ?? ""
        };
    }
}
