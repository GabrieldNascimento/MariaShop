namespace MariaShop.Api.Exceptions.InvalidData
{
    public class InvalidProductPriceException : InvalidDataException
    {
        public InvalidProductPriceException(decimal price)
            : base(
                message: $"Product price must be greater than zero. Provided: {price}.",
                code: "PRODUCT_INVALID_PRICE"
            ) {
        }
    }

}
