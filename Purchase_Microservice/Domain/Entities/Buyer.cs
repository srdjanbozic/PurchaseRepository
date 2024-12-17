using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Purchase_Microservice.Domain.Entities
{
    public class Buyer
    {
        public Guid BuyerId { get; set; }

        [Required(ErrorMessage = "Buyer username is required!")]
        [StringLength(50, ErrorMessage = "Buyer username cannot be longer than 50 characters!")]
        public string BuyerUsername { get; set; }

        [Required(ErrorMessage = "Buyer email is required!")]
        [EmailAddress(ErrorMessage = "Invalid email address format!")]
        public string BuyerEmail { get; set; }
        public Buyer(Guid guid, string username, string email)
        {
            BuyerId = guid;
            BuyerUsername = username;
            BuyerEmail = email;
        }
        public Buyer(){ }
    }
}
