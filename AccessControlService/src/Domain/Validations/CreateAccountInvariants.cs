using Domain.Models;
using FluentValidation;
using FluentValidation.Validators;

namespace Domain.Validations;

public class CreateAccountInvariants : AbstractValidator<Account>
{
    public CreateAccountInvariants()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Name is required")
            .MinimumLength(3)
            .WithMessage("Name must be at least 3 characters");

        RuleFor(x => x.Email)
            .EmailAddress(EmailValidationMode.Net4xRegex)
            .WithMessage("Invalid email");
    }
}