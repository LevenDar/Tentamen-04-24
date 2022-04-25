using Microsoft.EntityFrameworkCore;

namespace Tentamen.Models
{
    public class DataContext : DbContext
    {
        public DataContext()
        { 
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public virtual DbSet<CategoryEntity> Categories { get; set; } = null!;
        public virtual DbSet<ProductEntity> Products { get; set; } = null!;
        public virtual DbSet<UserEntity> Users { get; set; } = null!;
        public virtual DbSet<OrderEntity> Orders { get; set; } = null!;
    }
}
