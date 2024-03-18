using System.Net;
using Application.Controllers.Dtos;
using Application.Controllers.Mappers;
using Application.Services.Commands.Dtos;
using Common.Application;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("api/v1/account")]
public class GetAccountDetailsController : ControllerBase
{
    private readonly IApplicationCommandServiceWithResultAsync<GetAccountDetailsCommandDto, GetAccountDetailsCommandResultDto> _service;

    public GetAccountDetailsController(IApplicationCommandServiceWithResultAsync<GetAccountDetailsCommandDto, GetAccountDetailsCommandResultDto> service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpGet]
    [Route("{accountId:guid}")]
    [ProducesResponseType(typeof(GetAccountDetailsResponseDto), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> CreateAccount(Guid accountId, CancellationToken cancellationToken)
    {
        var result = await _service.ProcessAsync(new GetAccountDetailsCommandDto(accountId), cancellationToken);
        
        return Ok(result!.MapToCreateAccountResponse());
    }
}