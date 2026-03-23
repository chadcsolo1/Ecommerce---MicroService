using FluentValidation;
using Order.Commands;

namespace Order.Validators
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator() 
        {
            RuleFor(order => order.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
            RuleFor(order => order.UserName)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(70).WithMessage("{PropertyName} must not exceed 70 characters.");
            RuleFor(order => order.TotalPrice)
                .NotNull().WithMessage("{PropertyName} is required.")
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} should not be negative.");
            RuleFor(order => order.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .EmailAddress().WithMessage("{PropertyName} must be a valid email address.");
            RuleFor(order => order.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(order => order.LastName)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(order => order.CardNumber)
                .CreditCard().When(order => !string.IsNullOrEmpty(order.CardNumber))
                .WithMessage("{PropertyName} must be valid credit card number.");
            RuleFor(order => order.Expiration)
                .Matches(@"^(0[1-9]|1[0-2])\/?([0-9]{2})$")
                .When(order => !string.IsNullOrEmpty(order.Expiration))
                .WithMessage("{PropertyName} must be in MM/YY format");
            RuleFor(order => order.Cvv)
                .Matches(@"^\d{3,4}$")
                .When(order => !string.IsNullOrEmpty(order.Cvv))
                .WithMessage("{PropertyName} must be 3 or 4 digits.");
            RuleFor(order => order.PaymentMethod)
                .NotNull().WithMessage("{PropertyName} is required.")
                .InclusiveBetween(1, 3).WithMessage("{PropertyName} must be between 1 and 3.");
        }
    }
}
