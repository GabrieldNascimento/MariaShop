namespace MariaShop.Api.Exceptions.InvalidData
{
    public class InvalidProductNameException : InvalidDataException
    {
        public InvalidProductNameException()
            : base(
                message: "Product name is required.",
                code: "PRODUCT_NAME_REQUIRED"
            ) {
        }
    }

}
