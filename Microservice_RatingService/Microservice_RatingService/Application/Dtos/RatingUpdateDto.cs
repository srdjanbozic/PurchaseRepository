using Microservice_RatingService.Domain.Enums;

namespace Microservice_RatingService.Application.Dtos
{
    public class RatingUpdateDto
    {
        public int Grade { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public Status Status { get; set; }
        public DateTime RatingDate { get; set; }
    }
}
