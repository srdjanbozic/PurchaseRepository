using Microservice_RatingService.Domain.Enums;
using System.Text.Json.Serialization;

namespace Microservice_RatingService.Application.Dtos
{
    public class RatingCreateDto
    {
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
        public DateTime? PurchaseDate { get; set; }
        public decimal? PurchasePrice { get; set; }
    }
}

