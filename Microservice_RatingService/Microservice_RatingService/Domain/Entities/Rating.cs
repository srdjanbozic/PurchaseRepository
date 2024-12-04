using Microservice_RatingService.Domain.Enums;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microservice_RatingService.Domain.Entities
{
    [Table("rating")]
    public class Rating
    {
        [Key]
        [Column("RatingId")]
        [SwaggerSchema(ReadOnly = true)]
        public Guid RatingId { get; set; }

        [Required(ErrorMessage = "Rating date is required!")]
        [DataType(DataType.Date)]
        [Display(Name = "Rating Date")]
        public DateTime RatingDate { get; set; }

        [Required(ErrorMessage = "Grade is required!")]
        [Range(1, 5)]
        public int Grade { get; set; }

        [Required(ErrorMessage = "Comment is required!")]
        [StringLength(50, ErrorMessage = "Comment cannot be longer than 50 characters!")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Title is required!")]
        [StringLength(20, ErrorMessage = "Title cannot be longer than 20 characters!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Status is required!")]
        public Status Status { get; set; }

        /* Foreign key properties
        public Guid BuyerId { get; set; }
        public Guid SellerId { get; set; }
        public Guid PurchaseId { get; set; }
        */

        // Navigation properties
        public Buyer Buyer { get; set; }
        public Seller Seller { get; set; }
        public Purchase Purchase { get; set; }

    }
}
