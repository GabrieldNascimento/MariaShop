namespace MariaShop.Api.Exceptions.InvalidUse
{
    public class ProductNotFoundException : InvalidUseException
    {
        public ProductNotFoundException(Guid categoryId)
           : base(
               message: $"Category with id '{categoryId}' was not found.",
               code: "CATEGORY_NOT_FOUND") {
        }
    }
}
