using System.Net;
using Application.Controllers.Dtos;
using Application.Services.Commands.Dtos;
using Common.Application;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("api/v1/account")]
public class DeactivateAccountController : ControllerBase
{
    private readonly IApplicationCommandServiceAsync<DeactivateAccountCommandDto> _service;

    public DeactivateAccountController(IApplicationCommandServiceAsync<DeactivateAccountCommandDto> service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpPost]
    [Route("deactivate")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public async Task<IActionResult> DeactivateAccount([FromBody] DeactivateAccountRequestDto request, CancellationToken cancellationToken)
    {
        await _service.ProcessAsync(new DeactivateAccountCommandDto(request.accountId) , cancellationToken);
        
        return Ok();
    }
}