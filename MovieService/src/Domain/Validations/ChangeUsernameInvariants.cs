using Domain.Models;
using FluentValidation;

namespace Domain.Validations;

public class ChangeUsernameInvariants : AbstractValidator<Account> {}