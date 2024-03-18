using Domain.Models;
using FluentValidation;

namespace Domain.Validations;

public class CanChangeUsernameInvariants : AbstractValidator<Account>
{
    public CanChangeUsernameInvariants()
    {
        RuleFor(x => x.Active)
            .Equal(true)
            .WithMessage("The account must be active to change username!");
    }
}