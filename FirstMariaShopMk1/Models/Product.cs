using MariaShop.Api.Exceptions.InvalidData;
using MariaShop.Api.Exceptions.InvalidUse;

namespace MariaShop.Api.Models
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }
        public bool IsActive { get; private set; }
        public Guid CategoryId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected Product() { } // EF

        public Product(string name, decimal price, int initialStock, Guid categoryId, string? description = null) {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;

            SetName(name);
            SetPrice(price);
            SetInitialStock(initialStock);

            CategoryId = categoryId;
            Description = description;

            IsActive = initialStock > 0;
        }

        public void SetName(string name) {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidProductNameException();

            Name = name;
        }

        public void SetPrice(decimal price) {
            if (price <= 0)
                throw new InvalidProductPriceException(price);

            Price = price;
        }

        private void SetInitialStock(int quantity) {
            if (quantity < 0)
                throw new InvalidInitialStockException(quantity);

            StockQuantity = quantity;
        }

        public void IncreaseStock(int quantity) {
            if (quantity <= 0)
                throw new InvalidStockQuantityException(quantity);

            StockQuantity += quantity;
        }

        public void DecreaseStock(int quantity) {
            if (quantity <= 0)
                throw new InvalidStockQuantityException(quantity);

            if (StockQuantity < quantity)
                throw new InsufficientStockException(quantity, StockQuantity);

            StockQuantity -= quantity;

            if (StockQuantity == 0)
                IsActive = false;
        }

        public void SetDescription(string? description) {
            if (description is null) {
                Description = null;
                return;
            }

            description = description.Trim();

            if (description.Length == 0)
                throw new InvalidCategoryDescriptionException();

            if (description.Length > 150)
                throw new InvalidCategoryDescriptionException();

            Description = description;
        }

        public void Activate() {
            if (StockQuantity <= 0)
                throw new ProductWithoutStockActivationException(Id);

            IsActive = true;
        }

        public void Deactivate() {
            IsActive = false;
        }
    }


}
