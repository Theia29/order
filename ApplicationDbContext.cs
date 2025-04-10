using Microsoft.EntityFrameworkCore;
using OrderModuleV2;
using OrderModuleV2.Model;

namespace OrderModuleV2.Model

{
    
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>()
                .Property(c => c.Price)
                .HasPrecision(10, 2);  

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(12, 2);  

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Order)
                .WithMany(o => o.CartItems) 
                .HasForeignKey(c => c.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}