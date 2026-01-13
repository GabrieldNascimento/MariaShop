namespace MariaShop.Api.Exceptions.InvalidData
{
    public class InvalidStockQuantityException : InvalidDataException
    {
        public InvalidStockQuantityException(int quantity)
            : base(
                message: $"Stock quantity must be greater than zero. Provided: {quantity}.",
                code: "PRODUCT_INVALID_STOCK_QUANTITY"
            ) {
        }
    }

}
