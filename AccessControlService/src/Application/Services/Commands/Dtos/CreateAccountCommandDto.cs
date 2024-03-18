using Common.Application;

namespace Application.Services.Commands.Dtos;

public class CreateAccountCommandDto : IApplicationCommand
{
    public CreateAccountCommandDto(string name, string email)
    {
        Name = name;
        Email = email;
    }

    public string Name { get; private set; }
    public string Email { get; private set; }
}