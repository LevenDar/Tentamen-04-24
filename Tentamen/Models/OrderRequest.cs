namespace Tentamen.Models
{
    public class OrderRequest
    {
        public int AmountOfProducts { get; set; }
        public string OrderStatus { get; set; }

        public int UserId { get; set; }
    }
}
