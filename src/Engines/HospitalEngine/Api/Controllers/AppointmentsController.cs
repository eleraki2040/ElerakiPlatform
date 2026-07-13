using Eleraki.HospitalEngine.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eleraki.HospitalEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AppointmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> ScheduleAppointment([FromBody] ScheduleAppointmentCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest(result.Error);
        return CreatedAtAction(nameof(GetAppointmentById), new { id = result.Value }, result.Value);
    }

    [HttpGet("{id:guid}")]
    public Task<IActionResult> GetAppointmentById(Guid id, CancellationToken cancellationToken)
    {
        return Task.FromResult<IActionResult>(NotFound());
    }
}
