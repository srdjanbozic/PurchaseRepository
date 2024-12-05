using Microservice_RatingService.Domain.Entities;
using Microservice_RatingService.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Microservice_RatingService.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public AppDbContext() { }

        public virtual DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.OwnsOne(r => r.Buyer, b =>
                {
                    b.Property(x => x.BuyerId).HasColumnName("BuyerId");
                    b.Property(x => x.BuyerUsername).HasColumnName("BuyerUsername");
                    b.Property(x => x.BuyerEmail).HasColumnName("BuyerEmail");
                });

                entity.OwnsOne(r => r.Seller, s =>
                {
                    s.Property(x => x.SellerId).HasColumnName("SellerId");
                    s.Property(x => x.SellerUsername).HasColumnName("SellerUsername");
                    s.Property(x => x.SellerEmail).HasColumnName("SellerEmail");
                });

                entity.OwnsOne(r => r.Purchase, p =>
                {
                    p.Property(x => x.PurchaseId).HasColumnName("PurchaseId");
                    p.Property(x => x.PurchaseDate).HasColumnName("PurchaseDate");
                    p.Property(x => x.PurchasePrice).HasColumnName("PurchasePrice").HasPrecision(18, 2); ;
                });
            });
        }
    }

    public static class DbInitializer
    {
        public static async Task SeedData(AppDbContext context)
        {
            if (await context.Ratings.AnyAsync()) return;

            var ratings = new[]
            {
               new Rating
               {
                   RatingId = Guid.NewGuid(),
                   RatingDate = DateTime.Parse("2024-01-15"),
                   Grade = 5,
                   Comment = "Excellent product and fast delivery!",
                   Title = "Great Experience",
                   Status = Status.Completed,
                   Buyer = new Buyer(Guid.NewGuid(), "john_doe", "john.doe@email.com"),
                   Seller = new Seller(Guid.NewGuid(), "tech_store", "store@techstore.com"),
                   Purchase = new Purchase(Guid.NewGuid(), DateTime.Parse("2024-01-10"), 599.99m)
               },
               new Rating
               {
                   RatingId = Guid.NewGuid(),
                   RatingDate = DateTime.Parse("2024-02-01"),
                   Grade = 4,
                   Comment = "Good product but shipping was slow",
                   Title = "Satisfied Overall",
                   Status = Status.Completed,
                   Buyer = new Buyer(Guid.NewGuid(), "alice_smith", "alice.smith@email.com"),
                   Seller = new Seller(Guid.NewGuid(), "electronic_hub", "sales@ehub.com"),
                   Purchase = new Purchase(Guid.NewGuid(), DateTime.Parse("2024-01-25"), 299.50m)
               }
           };

            await context.Ratings.AddRangeAsync(ratings);
            await context.SaveChangesAsync();
        }
    }
}
