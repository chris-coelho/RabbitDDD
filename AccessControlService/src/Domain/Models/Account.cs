using Common.Domain;
using Common.Util;
using Domain.Validations;
using FluentValidation.Results;

namespace Domain.Models;

// ReSharper disable VirtualMemberCallInConstructor
public class Account : Aggregate
{
    public virtual string Username { get; protected set; }
    public virtual string Email { get; protected set; }
    public virtual DateTime CreatedOn { get; protected set; }
    public virtual bool Active { get; protected set; }

    protected Account() { }
    
    public Account(string username, string email, Guid id = default)
    {
        Id = id;
        Username = username;
        Email = email;
        CreatedOn = DateTime.Now.GetBrazilianTime();
        Active = true;

        CheckInvariants(this, new CreateAccountInvariants(), new List<ValidationResult>());
    }

    public virtual void Deactivate()
    {
        if (CheckInvariants(this, new CanDeactivateAccountInvariants(), 
                new List<ValidationResult>()) == InvariantResult.Failed) return;

        Active = false;
    }

    public virtual void ChangeUsername(string username)
    {
        if (CheckInvariants(this, new CanChangeUsernameInvariants(), 
                new List<ValidationResult>()) == InvariantResult.Failed) return;

        Username = username;

        CheckInvariants(this, new ChangeUsernameInvariants(), 
            new List<ValidationResult>());
        
        //TODO: Add event AccountUsernameChangedEvent to the Aggregate
        // AddDomainEvent(new AccountUsernameChangedEvent
        // {
        //     
        // });

    }

}