namespace Bookkeeping.Contracts.Models
{
    public sealed class PagedList<T>
    {
        public IReadOnlyList<T> Items { get; }

        public int TotalCount { get; }

        public int Page { get; }

        public int PageSize { get; }

        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        public bool HasPreviousPage => Page > 1;

        public bool HasNextPage => Page < TotalPages;

        public PagedList(IReadOnlyList<T> items, int totalCount, int page, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            Page = page;
            PageSize = pageSize;
        }

        // Удобный статический метод для создания
        public static PagedList<T> Create(IEnumerable<T> source, int page, int pageSize)
        {
            var total = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, total, page, pageSize);
        }
    }
}
