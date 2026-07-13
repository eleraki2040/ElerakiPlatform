using Eleraki.FinanceEngine.Application.Commands;
using Eleraki.FinanceEngine.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eleraki.FinanceEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransactionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest(result.Error);
        return CreatedAtAction(nameof(GetTransactionById), new { id = result.Value }, result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTransactionById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTransactionByIdQuery(id), cancellationToken);
        if (result is null) return NotFound();
        return Ok(result);
    }
}
