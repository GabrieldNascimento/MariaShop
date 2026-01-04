namespace FirstMariaShopMk1.Exceptions.InvalidData
{
    public class InvalidInitialStockException : InvalidDataException
    {
        public InvalidInitialStockException(int quantity)
            : base(
                message: $"Initial stock cannot be negative. Provided: {quantity}.",
                code: "PRODUCT_INVALID_INITIAL_STOCK"
            ) {
        }
    }

}
