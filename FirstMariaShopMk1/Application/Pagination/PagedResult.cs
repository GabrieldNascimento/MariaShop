namespace FirstMariaShopMk1.Application.Pagination
{
    public class PagedResult<T>
    {
        public IReadOnlyList<T> Items { get; }
        public int Page { get; }
        public int PageSize { get; }
        public int TotalItems { get; }

        public int TotalPages =>
            (int)Math.Ceiling((double)TotalItems / PageSize);

        public bool HasNextPage =>
            Page < TotalPages;

        public bool HasPreviousPage =>
            Page > 1;

        public PagedResult(
            IReadOnlyList<T> items,
            int page,
            int pageSize,
            int totalItems) {
            Items = items;
            Page = page;
            PageSize = pageSize;
            TotalItems = totalItems;
        }
    }

}
