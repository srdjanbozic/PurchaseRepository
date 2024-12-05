using System.ComponentModel.DataAnnotations.Schema;

namespace Microservice_RatingService.Domain.Entities
{
    public class Seller
    {
        public Guid SellerId { get; set; }
        public string SellerUsername { get; set; }
        public string SellerEmail { get; set; }
        [ForeignKey("Rating")]
        public Guid RatingId { get; set; }
        public Rating Rating { get; set; }

        public Seller(Guid sellerId, string username, string email)
        {
            SellerId = sellerId;
            SellerUsername = username;
            SellerEmail = email;
        }
        public Seller()
        {
        }
    }
}
