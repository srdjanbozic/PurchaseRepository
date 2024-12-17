using FluentValidation;
using Purchase_Microservice.Application.Dtos;

namespace Purchase_Microservice.Application.Validators
{
    public class PurchaseCreateDtoValidator : AbstractValidator<PurchaseCreateDto>
    {
        public PurchaseCreateDtoValidator()
        {
            RuleFor(x => x.PurchaseDate)
                .NotEmpty().WithMessage("Purchase date is required")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Purchase date cannot be in the future");

            RuleFor(x => x.PurchasePrice)
                .NotEmpty().WithMessage("Purchase price is required")
                .GreaterThan(0).WithMessage("Purchase price must be greater than 0");

            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Currency is required")
                .Length(3).WithMessage("Currency must be 3 characters long");

            RuleFor(x => x.BuyerId)
                .NotEmpty().WithMessage("Buyer ID is required");

            RuleFor(x => x.BuyerUsername)
                .NotEmpty().WithMessage("Buyer username is required")
                .MinimumLength(3).WithMessage("Buyer username must be at least 3 characters")
                .MaximumLength(50).WithMessage("Buyer username cannot exceed 50 characters");

            RuleFor(x => x.BuyerEmail)
                .NotEmpty().WithMessage("Buyer email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.DeliveryId)
                .NotEmpty().WithMessage("Delivery ID is required");

            RuleFor(x => x.DeliveryPrice)
                .NotEmpty().WithMessage("Delivery price is required")
                .GreaterThanOrEqualTo(0).WithMessage("Delivery price must be greater than or equal to 0");

            RuleFor(x => x.PostId)
                .NotEmpty().WithMessage("Post ID is required");

            RuleFor(x => x.PostTitle)
                .NotEmpty().WithMessage("Post title is required")
                .MaximumLength(100).WithMessage("Post title cannot exceed 100 characters");

            RuleFor(x => x.OwnerId)
                .NotEmpty().WithMessage("Owner ID is required");
        }
    }

    public class PurchaseUpdateDtoValidator : AbstractValidator<PurchaseUpdateDto>
    {
        public PurchaseUpdateDtoValidator()
        {
            RuleFor(x => x.PurchasePrice)
                .NotEmpty().WithMessage("Purchase price is required")
                .GreaterThan(0).WithMessage("Purchase price must be greater than 0");

            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Currency is required")
                .Length(3).WithMessage("Currency must be 3 characters long");

            RuleFor(x => x.DeliveryPrice)
                .NotEmpty().WithMessage("Delivery price is required")
                .GreaterThanOrEqualTo(0).WithMessage("Delivery price must be greater than or equal to 0");
        }
    }
}