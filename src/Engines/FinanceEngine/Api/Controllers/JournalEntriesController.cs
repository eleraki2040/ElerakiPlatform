using Eleraki.FinanceEngine.Application.Commands;
using Eleraki.FinanceEngine.Application.Queries;
using Eleraki.FinanceEngine.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eleraki.FinanceEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JournalEntriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public JournalEntriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateJournalEntry([FromBody] CreateJournalEntryCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest(result.Error);
        return CreatedAtAction(nameof(GetJournalEntryById), new { id = result.Value }, result.Value);
    }

    [HttpPost("{id:guid}/post")]
    public async Task<IActionResult> PostJournalEntry(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new PostJournalEntryCommand(JournalEntryId.From(id)), cancellationToken);
        if (!result.IsSuccess) return BadRequest(result.Error);
        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetJournalEntryById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetJournalEntryByIdQuery(id), cancellationToken);
        if (result is null) return NotFound();
        return Ok(result);
    }
}
