namespace MariaShop.Api.Options
{
    public sealed class JwtOptions
    {
        public string Key { get; init; } = null!;
        public string Issuer { get; init; } = null!;
        public string Audience { get; init; } = null!;
    }

}
