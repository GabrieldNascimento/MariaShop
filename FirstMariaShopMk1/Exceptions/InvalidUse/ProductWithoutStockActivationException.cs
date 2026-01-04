namespace FirstMariaShopMk1.Exceptions.InvalidUse
{
    public class ProductWithoutStockActivationException : InvalidUseException
    {
        public ProductWithoutStockActivationException(Guid productId)
            : base(
                message: $"Product {productId} cannot be activated without stock.",
                code: "PRODUCT_ACTIVATION_WITHOUT_STOCK"
            ) {
        }
    }

}
