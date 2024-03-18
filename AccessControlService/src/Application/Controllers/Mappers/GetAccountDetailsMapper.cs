using Application.Controllers.Dtos;
using Application.Services.Commands.Dtos;

namespace Application.Controllers.Mappers;

public static class GetAccountDetailsMapper
{
    public static GetAccountDetailsResponseDto MapToCreateAccountResponse(this GetAccountDetailsCommandResultDto result)
    {
        if (result is null)
            return new GetAccountDetailsResponseDto();
        
        return new GetAccountDetailsResponseDto
        {
            id = result.AccountId,
            username = result.Username,
            email = result.Email,
            active = result.Active,
            createdOn = result.CreatedOn
        };
    }
}