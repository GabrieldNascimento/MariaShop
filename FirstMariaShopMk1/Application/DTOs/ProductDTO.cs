namespace FirstMariaShopMk1.Application.DTOs
{
    //Sem CQRS
    public class ProductDTO
    {
        public Guid? Id { get; set; }

        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        public bool? IsActive { get; set; }

        public Guid CategoryId { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
