namespace FirstMariaShopMk1.Exceptions.InvalidData
{
    public class InvalidCategoryDescriptionException : InvalidDataException
    {
        public InvalidCategoryDescriptionException()
           : base(
               message: "Category description does not meet validation rules.",
               code: "CATEGORY_DESCRIPTION_INVALID"
           ) {
        }
    }
}
