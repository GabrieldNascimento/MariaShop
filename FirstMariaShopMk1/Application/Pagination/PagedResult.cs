namespace MariaShop.Api.Application.Pagination
{
    public sealed class PagedResult<T>
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

        private PagedResult(IReadOnlyList<T> items, int page, int pageSize, int totalItems) {
            Items = items;
            Page = page;
            PageSize = pageSize;
            TotalItems = totalItems;
        }

        public static PagedResult<T> Create(
            IReadOnlyList<T> items,
            int page,
            int pageSize,
            int totalItems) {
            if (items is null)
                throw new ArgumentNullException(nameof(items));

            if (page <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(page), "Page must be greater than zero.");

            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(pageSize), "PageSize must be greater than zero.");

            if (totalItems < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(totalItems), "TotalItems cannot be negative.");

            if (items.Count > pageSize)
                throw new ArgumentException(
                    "Items count cannot be greater than PageSize.");

            return new PagedResult<T>(
                items,
                page,
                pageSize,
                totalItems);
        }
    }


}
