using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microservice_RatingService.Domain.Entities
{
    public class Buyer
    {
        
        public Guid BuyerId { get; set; }
        [Required] 
        [StringLength(50, MinimumLength = 3)]
        public string BuyerUsername { get; set; }
        [Required] 
        [EmailAddress]
        public string BuyerEmail { get; set; }
        
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
