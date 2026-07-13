using Eleraki.HospitalEngine.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eleraki.HospitalEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdmissionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdmissionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAdmission([FromBody] CreateAdmissionCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest(result.Error);
        return CreatedAtAction(nameof(GetAdmissionById), new { id = result.Value }, result.Value);
    }

    [HttpGet("{id:guid}")]
    public Task<IActionResult> GetAdmissionById(Guid id, CancellationToken cancellationToken)
    {
        return Task.FromResult<IActionResult>(NotFound());
    }
}
