using System.Text.Json.Serialization;

namespace Microservice_RatingService.Application.Dtos
{
    public class SellerRatingStatsDto
    {
        [JsonRequired]
        public Guid SellerId { get; set; }
        [JsonRequired]
        public required string SellerUsername { get; set; }
        public double AverageRating { get; set; }
        public int TotalRatings { get; set; }
        public required Dictionary<int, int> RatingDistribution { get; set; } // Key: rating value (1-5), Value: count
    }
}
