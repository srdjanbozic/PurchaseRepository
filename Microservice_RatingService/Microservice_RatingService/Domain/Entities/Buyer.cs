using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microservice_RatingService.Domain.Entities
{
    public class Buyer
    {
        public Guid BuyerId { get; set; }
        [StringLength(50)]
        public string BuyerUsername { get; set; }
        public string BuyerEmail { get; set; }
        [ForeignKey("Rating")]
        public Guid RatingId { get; set; }
        public Rating Rating { get; set; }

        public Buyer(Guid guid, string username, string email)
        {
            BuyerId = guid;
            BuyerUsername = username;
            BuyerEmail = email;
        }

        public Buyer()
        {
        }
    }
}
