using Microsoft.EntityFrameworkCore;

namespace RetailCodingExercise.Models
{
    public partial class ProductContext : DbContext
    {
        public ProductContext()
        {
        }

        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasKey(e => e.ItemId)
                    .HasName("PK_dbo.CartItems");

                entity.HasIndex(e => e.ProductId)
                    .HasName("IX_ProductId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_dbo.CartItems_dbo.Products_ProductId");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK_dbo.Categories");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK_dbo.Products");

                entity.HasIndex(e => e.CategoryId)
                    .HasName("IX_CategoryID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_dbo.Products_dbo.Categories_CategoryID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
