using System.ComponentModel.DataAnnotations;

namespace Tentamen.Models
{
    public class CategoryEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public virtual ICollection<ProductEntity> Products { get; set; } = null!;

    }
}
