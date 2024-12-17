using System.ComponentModel.DataAnnotations;

namespace Purchase_Microservice.Domain.Entities
{
    public class Post
    {
        public Guid PostId { get; set; }

        [Required(ErrorMessage = "Post title is required!")]
        [StringLength(100, ErrorMessage = "Post title cannot be longer than 100 characters!")]
        public string PostTitle { get; set; }

        [Required(ErrorMessage = "Created date is required!")]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Price is required!")]
        [Range(0.01, 999999.99, ErrorMessage = "Price must be between 0.01 and 999999.99.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Owner ID is required!")]
        public Guid OwnerId { get; set; }

        [StringLength(50, ErrorMessage = "Owner name cannot be longer than 50 characters!")]
        public string? OwnerName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format!")]
        public string? OwnerEmail { get; set; }

        [Range(1000000000, 9999999999, ErrorMessage = "Owner phone must be a valid 10-digit phone number!")]
        public int? OwnerPhone { get; set; }

        public Post(Guid postId, string title, DateTime createdDate, decimal price, Guid ownerId, string? ownerName, string? ownerEmail, int? ownerPhone)
        {
            PostId = postId;
            PostTitle = title;
            CreatedDate = createdDate;
            Price = price;
            OwnerId = ownerId;
            OwnerName = ownerName;
            OwnerEmail = ownerEmail;
            OwnerPhone = ownerPhone;
        }

        public Post() { }
    }
}