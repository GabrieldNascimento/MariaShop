using MariaShop.Api.Exceptions.InvalidData;

namespace MariaShop.Api.Models
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
            if (string.IsNullOrEmpty(name)) {
                throw new InvalidCategoryNameException();
            }
            Name = name;
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
            IsActive = true;
        }

        public void Deactivate() {
            IsActive = false;
        }

    }
}
