namespace Purchase_Microservice.Application.Exceptions
{
    namespace Purchase_Microservice.Application.Exceptions
    {
        public class PurchaseNotFoundException : Exception
        {
            public PurchaseNotFoundException(Guid id)
                : base($"Purchase with ID {id} was not found.") { }
        }

        public class PurchaseValidationException : Exception
        {
            public List<string> Errors { get; }

            public PurchaseValidationException(string message, List<string> errors = null)
                : base(message)
            {
                Errors = errors ?? new List<string>();
            }
        }

        public class BuyerNotFoundException : Exception
        {
            public BuyerNotFoundException(Guid buyerId)
                : base($"Buyer with ID {buyerId} was not found.") { }
        }

        public class PostNotFoundException : Exception
        {
            public PostNotFoundException(Guid postId)
                : base($"Post with ID {postId} was not found.") { }
        }

        public class DeliveryNotFoundException : Exception
        {
            public DeliveryNotFoundException(Guid deliveryId)
                : base($"Delivery with ID {deliveryId} was not found.") { }
        }

        public class OwnerNotFoundException : Exception
        {
            public OwnerNotFoundException(Guid ownerId)
                : base($"Owner with ID {ownerId} was not found.") { }
        }
    }
}
