using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tentamen.Models
{
    public class ProductEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Articalnumber { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }
        public CategoryEntity Category { get; set; } = null!;
    }
}
