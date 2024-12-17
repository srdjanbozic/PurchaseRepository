using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Purchase_Microservice.Domain.Entities
{
    [Table("purchases")]
    public class Purchase
    {
        // Primary key
        [Key]
        [Column("PurchaseId")]
        [SwaggerSchema(ReadOnly = true)]
        public Guid PurchaseId { get; set; }

        // Purchase date
        [Required(ErrorMessage = "Purchase date is required!")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Purchase Date")]
        public DateTime PurchaseDate { get; set; }

        // Purchase price
        [Required(ErrorMessage = "PurchasePrice is required.")]
        [Range(0.01, 999999.99, ErrorMessage = "PurchasePrice must be between 0.01 and 999999.99.")]
        public decimal PurchasePrice { get; set; }

        // Currency
        [Required(ErrorMessage = "Currency is required!")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency must be exactly 3 characters!")]
        public string Currency { get; set; }

        // Navigational properties
        public Buyer Buyer { get; set; }
        public Post Post { get; set; }
        public Delivery Delivery { get; set; }
    }
}