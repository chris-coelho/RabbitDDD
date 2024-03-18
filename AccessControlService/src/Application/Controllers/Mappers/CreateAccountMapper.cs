using Application.Controllers.Dtos;
using Application.Services.Commands.Dtos;

namespace Application.Controllers.Mappers;

// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
public static class CreateAccountMapper
{
    public static CreateAccountCommandDto MapToCreateAccountCommand(this CreateAccountRequestDto request)
    {
        return new CreateAccountCommandDto(request.username, request.email);
    }
    
    public static CreateAccountResponseDto MapToCreateAccountResponse(this CreateAccountCommandResultDto result)
    {
        if (result is null)
            return new CreateAccountResponseDto();
        
        return new CreateAccountResponseDto
        {
            accountId = result.AccountId.ToString(),
            created_on = result.CreatedOn.ToString("yyyy-MM-ddThh:mm:ss")
        };
    }

}