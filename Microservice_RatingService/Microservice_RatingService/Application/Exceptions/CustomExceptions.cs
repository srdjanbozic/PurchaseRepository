namespace Microservice_RatingService.Application.Exceptions
{
    public class RatingNotFoundException : Exception
    {
        public RatingNotFoundException(Guid id)
            : base($"Rating with ID {id} was not found.") { }
    }
    public class RatingValidationException : Exception
    {
        public List<string> Errors { get; }

        public RatingValidationException(string message, List<string>? errors = null)
            : base(message)
        {
            Errors = errors ?? new List<string>();
        }
    }
    public class SellerNotFoundException : Exception
    {
        public SellerNotFoundException(Guid sellerId)
            : base($"Seller with ID {sellerId} was not found.") { }
    }
    public class BuyerNotFoundException : Exception
    {
        public BuyerNotFoundException(Guid buyerId)
            : base($"Buyer with ID {buyerId} was not found.") { }
    }
}
