namespace Tentamen.Models
{
    public class ProductRequest
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Articalnumber { get; set; }
        public string Categoryname { get; set; } = null!;
    }
}
