using Microservice_RatingService.Domain.Enums;
using System.Text.Json.Serialization;

namespace Microservice_RatingService.Application.Dtos
{
    public class RatingUpdateDto
    {
        [JsonRequired]
        public int Grade { get; set; }
        public string? Title { get; set; }
        public string? Comment { get; set; }
        [JsonRequired]
        public Status Status { get; set; }
        [JsonRequired]
        public DateTime RatingDate { get; set; }
    }
}
