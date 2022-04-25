using System.ComponentModel.DataAnnotations;

namespace Tentamen.Models
{
    public class OrderEntity
    { //Vem har beställt
      // När beställningen gjordes
      // Status på beställning!!!!!!!!!!!!!!!!!!!!!!!!!
      //Vilka produkter
      //Hur många produkter

        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public string OrderStatus { get; set; } = null!;

        [Required]
        public int AmountOfProducts { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
