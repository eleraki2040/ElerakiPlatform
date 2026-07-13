using Eleraki.SchoolManagementEngine.Application.Commands;
using Eleraki.SchoolManagementEngine.Application.DTOs;
using Eleraki.SchoolManagementEngine.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eleraki.SchoolManagementEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CoursesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest(result.Error);
        return CreatedAtAction(nameof(GetCourseById), new { id = result.Value }, result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCourseById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCourseByIdQuery(id), cancellationToken);
        if (result is null) return NotFound();
        return Ok(result);
    }
}
