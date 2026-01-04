namespace FirstMariaShopMk1.Exceptions.InvalidData
{
    public class InvalidCategoryNameException : InvalidDataException
    {
        public InvalidCategoryNameException()
            : base(
                message: "Category name is required.",
                code: "CATEGORY_NAME_REQUIRED"
            ) {
        }
    }

}
