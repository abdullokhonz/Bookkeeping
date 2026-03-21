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
        private List<TreeNode> _treeNodes = new();
        private HashSet<Guid> _expandedNodes = new();

        public class TreeNode
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = "";
            public string? Description { get; set; }
            public string? AccountNumber { get; set; }
            public AccountNature? Nature { get; set; }
            public NodeType Type { get; set; }
            public List<TreeNode> Children { get; set; } = new();
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
                    _treeNodes = BuildTree(catResponse.Data ?? new(), accResponse.Data ?? new());
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

        // 📌 ИСПРАВЛЕННЫЙ МЕТОД BuildTree
        private List<TreeNode> BuildTree(List<CategoryAccount5dTreeDto> categories, List<IfrsAccountTreeDto> accounts)
        {
            // 1. Строим дерево категорий (рекурсивно)
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

                if (parentNode == null)
                    rootCategories.Add(node);
                else
                    parentNode.Children.Add(node);

                foreach (var child in cat.Children ?? new())
                    AddCategoryNode(child, node);
            }

            foreach (var cat in categories)
                AddCategoryNode(cat);

            // 2. Рекурсивное преобразование счёта и его дочерних счетов (используем готовую иерархию из DTO)
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
                // Добавляем детей рекурсивно
                foreach (var child in acc.Children ?? new())
                {
                    node.Children.Add(ConvertAccount(child));
                }
                return node;
            }

            // 3. Привязываем корневые счета к категориям
            foreach (var acc in accounts)
            {
                var accountNode = ConvertAccount(acc);
                if (acc.CategoryAccountId != Guid.Empty && categoryNodes.TryGetValue(acc.CategoryAccountId, out var catNode))
                {
                    catNode.Children.Add(accountNode);
                }
                else
                {
                    // Если категория не найдена, добавляем счёт в корень (на один уровень с категориями)
                    rootCategories.Add(accountNode);
                }
            }

            return rootCategories;
        }

        // --------- RenderNode Method

        private void ToggleNode(Guid id)
        {
            if (_expandedNodes.Contains(id))
                _expandedNodes.Remove(id);
            else
                _expandedNodes.Add(id);
        }

        private void NavigateToDetail(TreeNode node)
        {
            if (node.Type == NodeType.Account)
                NavManager.NavigateTo($"/accounts/details/{node.Id}");
            else
                NavManager.NavigateTo($"/accounts/categories/details/{node.Id}");
        }

        private string GetNatureName(AccountNature nature) => nature switch
        {
            AccountNature.Active => "Активный",
            AccountNature.Passive => "Пассивный",
            AccountNature.ActivePassive => "Активно-пассивный",
            _ => nature.ToString()
        };
    }
}
