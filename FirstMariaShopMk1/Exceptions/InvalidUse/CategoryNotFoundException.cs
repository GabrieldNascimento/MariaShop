namespace MariaShop.Api.Exceptions.InvalidUse
{
    public sealed class CategoryNotFoundException : InvalidUseException
    {
        public CategoryNotFoundException(Guid categoryId)
            : base(
                message: $"Category with id '{categoryId}' was not found.",
                code: "CATEGORY_NOT_FOUND") {
        }
    }

}
