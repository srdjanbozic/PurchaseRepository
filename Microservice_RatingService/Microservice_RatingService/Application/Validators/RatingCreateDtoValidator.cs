using FluentValidation;
using Microservice_RatingService.Application.Dtos;

namespace Microservice_RatingService.Application.Validators
{
    public class RatingCreateDtoValidator : AbstractValidator<RatingCreateDto>
    {
        public RatingCreateDtoValidator()
        {
            RuleFor(x => x.RatingDate)
                .NotEmpty().WithMessage("Rating date is required")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Rating date cannot be in the future");

            RuleFor(x => x.Grade)
                .NotEmpty().WithMessage("Grade is required")
                .InclusiveBetween(1, 5).WithMessage("Grade must be between 1 and 5");

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Comment is required")
                .MaximumLength(50).WithMessage("Comment cannot be longer than 50 characters")
                .MinimumLength(5).WithMessage("Comment must be at least 5 characters");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(20).WithMessage("Title cannot be longer than 20 characters")
                .MinimumLength(3).WithMessage("Title must be at least 3 characters");

            RuleFor(x => x.BuyerId)
                .NotEmpty().WithMessage("Buyer ID is required");

            RuleFor(x => x.SellerId)
                .NotEmpty().WithMessage("Seller ID is required");

            RuleFor(x => x.PurchaseId)
                .NotEmpty().WithMessage("Purchase ID is required");

            RuleFor(x => x.PurchaseDate)
                .NotEmpty().WithMessage("Purchase date is required")
                .LessThan(x => x.RatingDate).WithMessage("Purchase date must be before rating date");

            RuleFor(x => x.PurchasePrice)
                .NotEmpty().WithMessage("Purchase price is required")
                .GreaterThan(0).WithMessage("Purchase price must be greater than 0");
        }
    }

    public class RatingUpdateDtoValidator : AbstractValidator<RatingUpdateDto>
    {
        public RatingUpdateDtoValidator()
        {
            RuleFor(x => x.Grade)
                .NotEmpty().WithMessage("Grade is required")
                .InclusiveBetween(1, 5).WithMessage("Grade must be between 1 and 5");

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Comment is required")
                .MaximumLength(50).WithMessage("Comment cannot be longer than 50 characters")
                .MinimumLength(5).WithMessage("Comment must be at least 5 characters");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(20).WithMessage("Title cannot be longer than 20 characters")
                .MinimumLength(3).WithMessage("Title must be at least 3 characters");

            RuleFor(x => x.RatingDate)
                .NotEmpty().WithMessage("Rating date is required")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Rating date cannot be in the future");
        }
    }
}

