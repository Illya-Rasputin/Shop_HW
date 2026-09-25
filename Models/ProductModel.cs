namespace Shop_HW.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; } = 0;
        public string? Image { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        public int? CategoryId { get; set; }
        public CategoryModel? Category { get; set; }
    }
}
