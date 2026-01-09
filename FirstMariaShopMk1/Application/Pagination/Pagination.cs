namespace FirstMariaShopMk1.Application.Pagination
{
    public sealed class Pagination
    {
        public int Page { get; }
        public int PageSize { get; }

        public int Offset =>
            (Page - 1) * PageSize;

        private Pagination(int page, int pageSize) {
            Page = page;
            PageSize = pageSize;
        }

        public static Pagination Create(int page, int pageSize) {
            if (page <= 0)
                throw new ArgumentOutOfRangeException(nameof(page));

            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize));

            return new Pagination(page, pageSize);
        }
    }

}
