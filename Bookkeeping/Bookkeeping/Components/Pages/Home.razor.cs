using MudBlazor;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bookkeeping.Components.Pages
{
    public partial class Home
    {
        public enum OrderStatus { Draft, Processed, Canceled }
        public record RecentOrder(DateTime Date, string Name, decimal Amount, OrderStatus Status);
        private class KpiItem
        {
            public string Title { get; set; } = "";
            public string Value { get; set; } = "";
            public string Icon { get; set; } = "";
            public Color Color { get; set; }
            public string Comparison { get; set; } = "";
            public bool IsIncrease { get; set; }
        }

        private int _selectedDays = 30;
        private string _selectedCategory = "Все";

        private List<RecentOrder> _allOrders = new();
        private List<RecentOrder> _filteredOrders = new();

        private List<KpiItem> _kpiMetrics = new();

        private List<ChartSeries<double>> _series = new();
        private string[] _xAxisLabels = Array.Empty<string>();
        private LineChartOptions _options = new LineChartOptions()
        {
            LineDisplayType = LineDisplayType.Area,
            ShowDataMarkers = true,
            ChartPalette = new[] { "#4CAF50" }
        };

        protected override void OnInitialized()
        {
            _allOrders = GenerateFakeDatabase();
            ApplyFiltersAndRecalculate();
        }

        private void OnDaysChanged(int days)
        {
            _selectedDays = days;
            ApplyFiltersAndRecalculate();
        }

        private void OnCategoryChanged(string category)
        {
            _selectedCategory = category;
            ApplyFiltersAndRecalculate();
        }

        private void ApplyFiltersAndRecalculate()
        {
            var cutoffDate = DateTime.Now.Date.AddDays(-_selectedDays);
            _filteredOrders = _allOrders
                .Where(o => o.Date.Date >= cutoffDate)
                .Where(o => _selectedCategory == "Все" || o.Name == _selectedCategory)
                .OrderByDescending(o => o.Date)
                .ToList();

            var totalAmount = _filteredOrders.Where(o => o.Status != OrderStatus.Canceled).Sum(o => o.Amount);
            var successCount = _filteredOrders.Count(o => o.Status == OrderStatus.Processed);
            var draftCount = _filteredOrders.Count(o => o.Status == OrderStatus.Draft);

            _kpiMetrics = new List<KpiItem>
        {
            new KpiItem { Title = "Поступления (TJS)", Value = totalAmount.ToString("N0"), Icon = Icons.Material.Filled.AccountBalanceWallet, Color = Color.Success, Comparison = "Факт", IsIncrease = true },
            new KpiItem { Title = "Успешных ПКО", Value = successCount.ToString(), Icon = Icons.Material.Filled.CheckCircleOutline, Color = Color.Primary, Comparison = "Проведено", IsIncrease = true },
            new KpiItem { Title = "Черновики", Value = draftCount.ToString(), Icon = Icons.Material.Filled.EditNote, Color = Color.Warning, Comparison = "Требуют внимания", IsIncrease = false },
            new KpiItem { Title = "Справочники", Value = "4 Активны", Icon = Icons.Material.Filled.Storage, Color = Color.Info, Comparison = "Мастер-данные", IsIncrease = true },
        };

            if (_filteredOrders.Any())
            {
                var groupedByDate = _filteredOrders
                    .GroupBy(o => o.Date.Date)
                    .OrderBy(g => g.Key)
                    .ToList();

                _xAxisLabels = groupedByDate.Select(g => g.Key.ToString("dd.MM")).ToArray();
                _series = new List<ChartSeries<double>>
            {
                new ChartSeries<double>
                {
                    Name = _selectedCategory == "Все" ? "Объем" : _selectedCategory,
                    Data = groupedByDate.Select(g => (double)g.Where(x => x.Status != OrderStatus.Canceled).Sum(x => x.Amount)).ToArray()
                }
            };
            }
            else
            {
                _xAxisLabels = Array.Empty<string>();
                _series = new List<ChartSeries<double>>();
            }
        }

        private List<RecentOrder> GenerateFakeDatabase()
        {
            var list = new List<RecentOrder>();
            var random = new Random();
            var categories = new[] { "Оплата от покупателей", "Возврат аванса", "Взнос учредителя", "Розничная выручка" };

            for (int i = 0; i < 150; i++)
            {
                var date = DateTime.Now.AddDays(-random.Next(0, 31)).AddHours(-random.Next(0, 24));
                var cat = categories[random.Next(categories.Length)];
                var statusRoll = random.Next(100);
                var status = statusRoll < 75 ? OrderStatus.Processed : (statusRoll < 90 ? OrderStatus.Draft : OrderStatus.Canceled);
                var amount = (decimal)(random.Next(5, 300) * 100);
                list.Add(new RecentOrder(date, cat, amount, status));
            }
            return list;
        }

        private string GetStatusName(OrderStatus status) => status switch
        {
            OrderStatus.Draft => "Черновик",
            OrderStatus.Processed => "Проведен",
            OrderStatus.Canceled => "Отменен",
            _ => status.ToString()
        };

        private Color GetStatusColor(OrderStatus status) => status switch
        {
            OrderStatus.Processed => Color.Success,
            OrderStatus.Draft => Color.Warning,
            OrderStatus.Canceled => Color.Error,
            _ => Color.Default
        };

        private string GetMetricColor(Color color) => $"var(--mud-palette-{color.ToString().ToLower()})";
    }
}
