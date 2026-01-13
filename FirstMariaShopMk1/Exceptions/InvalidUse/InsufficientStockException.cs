namespace MariaShop.Api.Exceptions.InvalidUse
{
    public class InsufficientStockException : InvalidUseException
    {
        public InsufficientStockException(int quantity, int stock)
            : base(
                message: $"Insufficient stock. Provided: Quantity: {quantity} / Stock Quantity: {stock}.",
                code: "PRODUCT_INVALID_INITIAL_STOCK"
            ) {
        }
    }
}
