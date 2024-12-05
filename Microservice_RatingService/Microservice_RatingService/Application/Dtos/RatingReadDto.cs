using Microservice_RatingService.Domain.Enums;
using System.Text.Json.Serialization;

namespace Microservice_RatingService.Application.Dtos
{
    public class RatingReadDto
    {
        [JsonRequired]
        public Guid RatingId { get; set; }
        [JsonRequired]
        public DateTime RatingDate { get; set; }
        [JsonRequired]
        public int Grade { get; set; }
        public string? Comment { get; set; }
        public string? Title { get; set; }
        [JsonRequired]
        public Status Status { get; set; }
        [JsonRequired]
        public Guid BuyerId { get; set; }
        [JsonRequired]
        public Guid SellerId { get; set; }
        [JsonRequired]
        public Guid PurchaseId { get; set; }
    }
}
