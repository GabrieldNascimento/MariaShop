using FirstMariaShopMk1.Exceptions.Domain;
using System.Net.Sockets;
namespace FirstMariaShopMk1.Models
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
                throw new DomainException("Product name is required");

            Name = name;
        }

        public void SetPrice(decimal price) {
            if (price <= 0)
                throw new DomainException("Price must be greater than zero");

            Price = price;
        }

        private void SetInitialStock(int quantity) {
            if (quantity < 0)
                throw new DomainException("Initial stock cannot be negative");

            StockQuantity = quantity;
        }

        public void IncreaseStock(int quantity) {
            if (quantity <= 0)
                throw new DomainException("Quantity must be positive");

            StockQuantity += quantity;
        }

        public void DecreaseStock(int quantity) {
            if (quantity <= 0)
                throw new DomainException("Quantity must be positive");

            if (StockQuantity < quantity)
                throw new DomainException("Insufficient stock");

            StockQuantity -= quantity;

            if (StockQuantity == 0)
                IsActive = false;
        }

        public void Activate() {
            if (StockQuantity <= 0)
                throw new DomainException("Cannot activate product without stock");

            IsActive = true;
        }

        public void Deactivate() {
            IsActive = false;
        }
    }


}
