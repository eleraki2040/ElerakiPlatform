using Eleraki.HospitalEngine.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eleraki.HospitalEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly IMediator _mediator;

    public InvoicesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> GenerateInvoice([FromBody] GenerateInvoiceCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest(result.Error);
        return CreatedAtAction(nameof(GetInvoiceById), new { id = result.Value }, result.Value);
    }

    [HttpGet("{id:guid}")]
    public Task<IActionResult> GetInvoiceById(Guid id, CancellationToken cancellationToken)
    {
        return Task.FromResult<IActionResult>(NotFound());
    }
}
