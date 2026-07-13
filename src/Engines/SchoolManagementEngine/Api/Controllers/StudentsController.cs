using Eleraki.SchoolManagementEngine.Application.Commands;
using Eleraki.SchoolManagementEngine.Application.DTOs;
using Eleraki.SchoolManagementEngine.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eleraki.SchoolManagementEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudent([FromBody] CreateStudentCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest(result.Error);
        return CreatedAtAction(nameof(GetStudentById), new { id = result.Value }, result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetStudentById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetStudentByIdQuery(id), cancellationToken);
        if (result is null) return NotFound();
        return Ok(result);
    }
}
