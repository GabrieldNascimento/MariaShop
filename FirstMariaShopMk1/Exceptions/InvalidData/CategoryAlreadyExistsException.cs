using FirstMariaShopMk1.Exceptions.InvalidUse;

namespace FirstMariaShopMk1.Exceptions.InvalidData
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
