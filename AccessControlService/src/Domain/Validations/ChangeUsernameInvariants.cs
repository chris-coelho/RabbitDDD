using Domain.Models;
using FluentValidation;

namespace Domain.Validations;

public class ChangeUsernameInvariants : AbstractValidator<Account>
{
    public ChangeUsernameInvariants()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Name is required")
            .MinimumLength(3)
            .WithMessage("Name must be at least 3 characters");
    }
}