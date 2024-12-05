using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microservice_RatingService.Domain.Entities
{
    public class Seller
    {
        
        public Guid SellerId { get; set; }
        [Required]
        [StringLength(50)]
        public string SellerUsername { get; set; }
        [Required]
        [EmailAddress]
        public string SellerEmail { get; set; }
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
