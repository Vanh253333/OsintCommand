namespace OsintCommand.API.Response
{
    public class PaginatedData<T>
    {
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
        public long TotalItems { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public PaginatedData(IEnumerable<T> items, long totalItems, int page, int pageSize)
        {
            Items = items;
            TotalItems = totalItems;
            Page = page;
            PageSize = pageSize;
        }
    }
}
