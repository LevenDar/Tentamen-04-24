namespace Tentamen.Models
{
    public class Order
    {
 
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public int AmountOfProducts { get; set; }
        public int UserId { get; set; }
    }
}
