namespace Huddle_CRUD.Core.Common
{
    public class PaginatedResponse<T>
    {
        public T Result { get; set; }

        public int TotalResultCount { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int CurrentPageSize { get; set; } = 5;

        public bool HasPreviousPage => CurrentPage > 1;

        public bool HasNextPage => CurrentPage < TotalPages;

        public int TotalPages
        {
            get
            {
                if (CurrentPageSize == 0)
                    return 0;

                var totalPages = TotalResultCount / CurrentPageSize;

                if (TotalResultCount % CurrentPageSize > 0)
                    totalPages += 1;

                return totalPages;
            }
        }
    }
}
