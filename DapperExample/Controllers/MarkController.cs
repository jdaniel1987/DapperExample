using DapperExample.Infrastructure.Models;
using DapperExample.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DapperExample.Controllers;

[ApiController]
[Route("[controller]")]
public class MarkController : ControllerBase
{
    private readonly MarkRepository _markRepository;

    public MarkController(MarkRepository markRepository)
    {
        _markRepository = markRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetMarks()
    {
        var marks = await _markRepository.GetMarks();
        return Ok(marks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMark(int id)
    {
        var mark = await _markRepository.GetMark(id);

        if (mark == null)
            return NotFound();

        return Ok(mark);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMark([FromBody] Mark mark)
    {
        var createdMarkId = await _markRepository.CreateMark(mark);

        return CreatedAtAction(nameof(GetMark), new { id = createdMarkId }, mark);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMark(int id, [FromBody] Mark mark)
    {
        await _markRepository.UpdateMark(mark, id);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMark(int id)
    {
        await _markRepository.DeleteMark(id);

        return NoContent();
    }
}
