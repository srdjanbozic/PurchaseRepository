using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microservice_RatingService.Domain.Entities
{
    public class Purchase
    {
        
        public Guid PurchaseId { get; set; }
        [Required(ErrorMessage = "Purchase date is required!")]
        [DataType(DataType.Date)]
        [Display(Name = "Purchase Date")]
        public DateTime PurchaseDate { get; set; }
        [Required(ErrorMessage = "Price is required!")]
        public decimal PurchasePrice { get; set; }
        public Purchase(Guid guid, DateTime dateTime, decimal price)
        {
            PurchaseId = guid;
            PurchaseDate = dateTime;
            PurchasePrice = price;
        }
        public Purchase()
        {
        }
    }
}
