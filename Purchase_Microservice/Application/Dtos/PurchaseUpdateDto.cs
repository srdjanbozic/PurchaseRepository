namespace Purchase_Microservice.Application.Dtos
{
    public class PurchaseUpdateDto
    {
        public decimal PurchasePrice { get; set; }
        public string Currency { get; set; }
        public decimal DeliveryPrice { get; set; }
    }
}
