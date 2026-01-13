using MariaShop.Api.Exceptions.InvalidUse;

namespace MariaShop.Api.Exceptions.InvalidData
{
    public sealed class CategoryAlreadyExistsException : InvalidUseException
    {
        public CategoryAlreadyExistsException(string name)
            : base(
                message: $"Category '{name}' already exists.",
                code: "CATEGORY_ALREADY_EXISTS") {
        }
    }

}
