using Application.Note.Services;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace CloudDrive.Controllers;

[ApiController]
[Route("api/test")]
public class NoteController : ControllerBase
{
    private readonly INoteService _noteService;

    public NoteController(INoteService noteService)
    {
        _noteService = noteService;
    }

    [HttpGet("{id}")]
    public string GetUserName(int id)
    {
        return "User " + id;
    }

    [HttpPost]
    [Route("create/node")]
    public async Task<IActionResult> Add([FromBody] CNote note)
    {
        try
        {
            await _noteService.AddNote(note);
        }
        catch (Exception exception)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpGet]
    [Route("subnotes/{id}")]
    public Task<CNote> GetNode(string id)
    {
        try
        {
            return _noteService.GetNote(id);
        }
        catch (Exception exception)
        {
            throw new FileNotFoundException();
        }
    }
}