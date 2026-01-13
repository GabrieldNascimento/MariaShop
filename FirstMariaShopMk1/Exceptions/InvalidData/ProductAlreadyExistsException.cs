namespace MariaShop.Api.Exceptions.InvalidData
{
    public class ProductAlreadyExistsException : InvalidDataException
    {
        public ProductAlreadyExistsException(string name)
           : base(
               message: $"Category '{name}' already exists.",
               code: "CATEGORY_ALREADY_EXISTS") {
        }
    }
}
