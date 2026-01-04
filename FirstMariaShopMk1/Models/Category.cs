using FirstMariaShopMk1.Exceptions.Domain;
using System.Collections.ObjectModel;

namespace FirstMariaShopMk1.Models
{
    public class Category
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }    
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string? Description { get; private set; }

        protected Category() { }

        public Category(string name, string? description) {
            Id = Guid.NewGuid();
            SetName(name);
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            Description = description;
        }

        public void SetName(string name) {
            if (String.IsNullOrEmpty(name)) {
                throw new DomainException("O nome não pode ser null");
            }
            Name = name;
        }

        public void Activate() {
            IsActive = true;
        }

        public void Deactivate() {
            IsActive = false;
        }

    }
}
