using Domain.Models;
using FluentValidation;

namespace Domain.Validations;

public class CreateAccountInvariants : AbstractValidator<Account>
{
    public CreateAccountInvariants()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required");
    }
}