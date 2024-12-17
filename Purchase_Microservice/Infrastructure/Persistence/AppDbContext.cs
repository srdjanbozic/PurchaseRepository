using Microsoft.EntityFrameworkCore;
using Purchase_Microservice.Domain.Entities;

namespace Purchase_Microservice.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public AppDbContext() { }
        public virtual DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Purchase>(entity =>
            {
                // Set precision for PurchasePrice
                entity.Property(p => p.PurchasePrice)
                    .HasPrecision(18, 2);

                entity.OwnsOne(r => r.Buyer, b =>
                {
                    b.Property(x => x.BuyerId).HasColumnName("BuyerId");
                    b.Property(x => x.BuyerUsername).HasColumnName("BuyerUsername");
                    b.Property(x => x.BuyerEmail).HasColumnName("BuyerEmail");
                });

                entity.OwnsOne(r => r.Delivery, s =>
                {
                    s.Property(x => x.DeliveryId).HasColumnName("DeliveryId");
                    s.Property(x => x.DeliveryPrice).HasColumnName("DeliveryPrice")
                        .HasPrecision(18, 2);
                });

                entity.OwnsOne(r => r.Post, p =>
                {
                    p.Property(x => x.PostId).HasColumnName("PostId");
                    p.Property(x => x.CreatedDate).HasColumnName("PostDate");
                    p.Property(x => x.Price).HasColumnName("PostPrice")
                        .HasPrecision(18, 2);
                    p.Property(x => x.OwnerId).HasColumnName("OwnerId");
                    p.Property(x => x.OwnerName).HasColumnName("OwnerName");
                    p.Property(x => x.OwnerEmail).HasColumnName("OwnerEmail");
                    p.Property(x => x.OwnerPhone).HasColumnName("OwnerPhone");
                });
            });
        }
    }

    public static class DbInitializer
    {
        public static async Task SeedData(AppDbContext context)
        {
            try
            {
                Console.WriteLine("Checking if data exists...");
                var hasData = context.Purchases.Any();
                Console.WriteLine($"Has existing data: {hasData}");

                if (!hasData)
                {
                    Console.WriteLine("No data found, creating seed data...");
                    var purchases = new[]
                    {
                    new Purchase
                    {
                        PurchaseId = Guid.NewGuid(),
                        PurchaseDate = DateTime.Parse("2024-01-05"),
                        PurchasePrice = 522.2m,
                        Currency = "rsd",
                        Buyer = new Buyer(Guid.NewGuid(),"john wick","johnwickreal@gmail.com"),
                        Delivery = new Delivery(Guid.NewGuid(),122.2m),
                        Post = new Post(Guid.NewGuid(),"Nice purchase",DateTime.Parse("2024-01-10"),155.2m, Guid.NewGuid(),"Peter Jan","peterjan@gmail.com",06425254)
                    },
                    new Purchase
                    {
                        PurchaseId = Guid.NewGuid(),
                        PurchaseDate = DateTime.Parse("2024-02-05"),
                        PurchasePrice = 22.2m,
                        Currency = "eur",
                        Buyer = new Buyer(Guid.NewGuid(),"meril ","merils@gmail.com"),
                        Delivery = new Delivery(Guid.NewGuid(),12.2m),
                        Post = new Post(Guid.NewGuid(),"Nice",DateTime.Parse("2024-01-10"),155.2m, Guid.NewGuid(),"Jan","jan@gmail.com",06525254)
                    }
                };

                    await context.Purchases.AddRangeAsync(purchases);
                    Console.WriteLine("Added purchases to context, saving changes...");
                    var result = await context.SaveChangesAsync();
                    Console.WriteLine($"Saved {result} purchases to database.");
                }
                else
                {
                    Console.WriteLine("Data already exists, skipping seed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SeedData: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }
    }
}