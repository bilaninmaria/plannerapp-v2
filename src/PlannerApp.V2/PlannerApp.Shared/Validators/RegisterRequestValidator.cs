using FluentValidation;
using PlannerApp.Shared.Models;

namespace PlannerApp.Shared.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Email is required.");

            RuleFor(p => p.FirstName)
                .NotEmpty()
                .WithMessage("First name is required.")
                .MaximumLength(25)
                .WithMessage("First name must be less then 25 characters.");

            RuleFor(p => p.LastName)
               .NotEmpty()
               .WithMessage("Last name is required.")
               .MaximumLength(25)
               .WithMessage("Last name must be less then 25 characters.");

            RuleFor(p => p.Password)
               .NotEmpty()
               .WithMessage("Password is required.")
               .MinimumLength(5)
               .WithMessage("Password must have at least 5 characters.");

            RuleFor(p => p.ConfirmPassword)
              .Equal(p => p.Password)
              .WithMessage("Confirm password doesn't match the password.");
        }
    }
}