using E_Procurement.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Procurement.Repositories;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<ProductPrice> ProductPrices => Set<ProductPrice>();
    public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<PurchaseDetail> PurchaseDetails => Set<PurchaseDetail>();
    public DbSet<Purchase> Purchases => Set<Purchase>();
    
    protected AppDbContext(){}
    public AppDbContext(DbContextOptions options) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(builder =>
        {
            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.PhoneNumber).IsUnique();
        });
        modelBuilder.Entity<ProductPrice>(builder => builder.HasIndex(p => p.ProductCode).IsUnique());

        modelBuilder.Entity<ProductCategory>().HasData(
            
            new ProductCategory {Id = Guid.Parse("00c644b0-c027-41b0-99f9-5973995d8394"), Name = "Perabotan Rumah Tangga"},
            new ProductCategory {Id = Guid.Parse("05a254b6-0960-4dd1-a1a0-66028ee6990b"), Name = "Otomotif"},
            new ProductCategory {Id = Guid.Parse("19ce01d3-23a0-46cb-85c4-cef8206ddf11"), Name = "Peralatan Sekolah"},
            new ProductCategory {Id = Guid.Parse("745c1ba7-e4c6-4bbb-b6cb-28b082707fb3"), Name = "Elektronik"},
            new ProductCategory {Id = Guid.Parse("bdc428c1-01fb-40f6-928d-44a3f258ecc3"), Name = "Makanan/Minuman"},
            new ProductCategory {Id = Guid.Parse("eaf3509a-8d38-479f-840a-b418d6cf7513"), Name = "Pakaian"},
            new ProductCategory {Id = Guid.Parse("a95d824a-2b44-4b9b-a1c3-0afa4da9ba14"), Name = "Lainya"}
        );
    }
    
    
}