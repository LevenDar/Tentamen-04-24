using System.ComponentModel.DataAnnotations;

namespace Tentamen.Models
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Firstname { get; set; } = null!;

        [Required]
        public string Lastname { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;
    }
}
