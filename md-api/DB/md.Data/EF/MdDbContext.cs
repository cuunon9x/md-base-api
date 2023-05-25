using md.Data.Configurations;
using md.Data.Entites;
using Microsoft.EntityFrameworkCore;

namespace md.Data.EF
{
    public class MdDbContext : DbContext
    {
        public MdDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure using Fluent API
            modelBuilder.ApplyConfiguration(new ShopConfigurations());
            modelBuilder.ApplyConfiguration(new ProductConfigurations());
            modelBuilder.ApplyConfiguration(new ConsumerConfigurations());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Consumer> Consumers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductInShop> ProductsInShop { get; set; }
    }
}
