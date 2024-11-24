using Microsoft.EntityFrameworkCore;
using StoreManagementApp.Models;

namespace StoreManagementApp.Data
{
    public class StoreContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Report> Reports { get; set; }

        // Constructor accepting DbContextOptions
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        // Override OnModelCreating to configure the relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and other entity rules

            // Product-Transaction relationship (optional, because a product may exist without a transaction)
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Product) // Each transaction is associated with a product
                .WithMany(p => p.Transactions) // Each product can have many transactions
                .HasForeignKey(t => t.ProductId) // Foreign key is ProductId in Transaction
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false); // Make this relationship optional (no need for a transaction on product creation)


            // Mark Report as a keyless entity (Report doesn't have a primary key)
            modelBuilder.Entity<Report>().HasNoKey();
        }
    }
}
