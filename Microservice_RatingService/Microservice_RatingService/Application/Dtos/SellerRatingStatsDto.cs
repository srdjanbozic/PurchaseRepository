namespace Microservice_RatingService.Application.Dtos
{
    public class SellerRatingStatsDto
    {
        public Guid SellerId { get; set; }
        public string SellerUsername { get; set; }
        public double AverageRating { get; set; }
        public int TotalRatings { get; set; }
        public Dictionary<int, int> RatingDistribution { get; set; } // Key: rating value (1-5), Value: count
    }
}
