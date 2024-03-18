using Application.Controllers.Dtos;
using Application.Services.Commands.Dtos;

namespace Application.Controllers.Mappers;

public static class ChangeUsernameAccountMapper
{
    public static ChangeUsernameAccountCommandDto MapToChangeUsernameAccountCommand(this ChangeUsernameAccountRequestDto request)
    {
        return new ChangeUsernameAccountCommandDto(request.accountId, request.username);
    }
}