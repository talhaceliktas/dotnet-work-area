namespace ProductHub.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        // İhtiyacına göre Category vs. ekleyebilirsin
    }
}