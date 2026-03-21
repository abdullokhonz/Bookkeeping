namespace Bookkeeping.Contracts.Common.Responses
{
    public class PaginationMetadata
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int TotalCount { get; set; }

        public int PageSize { get; set; }

        public bool HasNext => CurrentPage < TotalPages;

        public bool HasPrevious => CurrentPage > 1;

        public PaginationMetadata(
            int totalCount,
            int pageSize,
            int currentPage,
            int totalPages)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPages = totalPages;
        }
    }
}
