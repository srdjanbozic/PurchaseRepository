using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microservice_RatingService.Domain.Entities
{
    public class Purchase
    {
        
        public Guid PurchaseId { get; set; }
        public DateTime PurchaseDate { get; set; }
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
