using System.Net;
using Application.Controllers.Dtos;
using Application.Controllers.Mappers;
using Application.Services.Commands.Dtos;
using Common.Application;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("api/v1/account")]
public class ChangeUsernameAccountController : ControllerBase
{
    private readonly IApplicationCommandServiceAsync<ChangeUsernameAccountCommandDto> _service;

    public ChangeUsernameAccountController(IApplicationCommandServiceAsync<ChangeUsernameAccountCommandDto> service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpPut]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public async Task<IActionResult> ChangeUsernameAccount([FromBody] ChangeUsernameAccountRequestDto request, CancellationToken cancellationToken)
    {
        await _service.ProcessAsync(request.MapToChangeUsernameAccountCommand(), cancellationToken);
        
        return Ok();
    }
}