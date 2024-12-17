namespace Purchase_Microservice.Application.Dtos
{
    public class PurchaseReadDto
    {
        public Guid PurchaseId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public string Currency { get; set; }

        // Buyer info
        public Guid BuyerId { get; set; }
        public string BuyerUsername { get; set; }
        public string BuyerEmail { get; set; }

        // Delivery info
        public Guid DeliveryId { get; set; }
        public decimal DeliveryPrice { get; set; }

        // Post info
        public Guid PostId { get; set; }
        public string PostTitle { get; set; }
        public DateTime PostDate { get; set; }
        public decimal PostPrice { get; set; }
        public Guid OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string OwnerEmail { get; set; }
        public int? OwnerPhone { get; set; }
    }
}
