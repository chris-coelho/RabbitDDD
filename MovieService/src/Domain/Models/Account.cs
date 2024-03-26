using Common.Domain;
using Domain.Validations;
using FluentValidation.Results;

namespace Domain.Models;

public class Account : Aggregate
{
    public virtual string Username { get; protected set; }

    protected Account() {}

    public Account(string username, Guid id = default)
    {
        Id = id;
        Username = username;

        CheckInvariants<Account>(this, new CreateAccountInvariants(), new List<ValidationResult>());
    }
    
    public virtual void ChangeUsername(string username)
    {
        Username = username;

        CheckInvariants(this, new ChangeUsernameInvariants(), 
            new List<ValidationResult>());
    }
}