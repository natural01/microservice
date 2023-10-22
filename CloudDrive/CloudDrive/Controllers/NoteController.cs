using Application.Foundations;
using Application.Notes.Services;
using CloudDrive.Dto;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace CloudDrive.Controllers;

[ApiController]
[Route("api")]
public class NoteController : ControllerBase
{
    private readonly INoteService _noteService;
    private readonly IUnitOfWork _unitOfWork;

    public NoteController(INoteService noteService, IUnitOfWork unitOfWork)
    {
        _noteService = noteService;
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    [Route("notes")]
    public async Task<IActionResult> AddNote([FromBody] CreateNoteDto note)
    {
        try
        {
            await _noteService.AddNote(note.ToDomain());
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
        _unitOfWork.Commit();

        return Ok();
    }

    [HttpGet]
    [Route("notes/{id}")]
    public Task<Note> GetNode(string id)
    {
        try
        {
            return _noteService.GetNote(id);
        }
        catch (Exception exception)
        {
            throw new Exception("Can't be found");
        }
    }

    [HttpDelete]
    [Route("notes/{id}")]
    public async Task<IActionResult> DeleteNode(string id)
    {
        try
        {
            await _noteService.DeleteNote(id);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
        _unitOfWork.Commit();
        return Ok();
    }

    [HttpPut]
    [Route("notes/{id}")]
    public async Task<IActionResult> UpdateNote([FromRoute] string id, [FromBody] CreateNoteDto note)
    {
        Note updatedNote = note.ToDomain();
        updatedNote.Id = id;

        try
        {
            await _noteService.UpdateNote(updatedNote);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
        _unitOfWork.Commit();

        return Ok();
    }
}