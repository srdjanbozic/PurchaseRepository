using System.ComponentModel.DataAnnotations;

namespace Purchase_Microservice.Domain.Entities
{
    public class Delivery
    {
        public Guid DeliveryId { get; set; }

        [Required(ErrorMessage = "Delivery price is required!")]
        [Range(0.01, 999999.99, ErrorMessage = "Delivery price must be between 0.01 and 999999.99.")]
        public decimal DeliveryPrice { get; set; }

        public Delivery(Guid guid, decimal price)
        {
            DeliveryId = guid;
            DeliveryPrice = price;
        }

        public Delivery() { }
    }
}