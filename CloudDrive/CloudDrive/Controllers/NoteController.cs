using Application.Foundations;
using Application.Notes.Services;
using CloudDrive.Authentication;
using CloudDrive.Dto;
using CloudDrive.Utilities;
using Domain;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace CloudDrive.Controllers;

[ApiController]
[Route("api")]
[ApiKeyAuthFilter]
public class NoteController : ControllerBase
{
    private readonly INoteService _noteService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<NoteDto> _noteDtoValidator;

    public NoteController(INoteService noteService, 
        IUnitOfWork unitOfWork,
        IValidator<NoteDto> createNoteDtoValidator)
    {
        _noteService = noteService;
        _unitOfWork = unitOfWork;
        _noteDtoValidator = createNoteDtoValidator;
    }

    [HttpPost]
    [Route("notes")]
    public async Task<IActionResult> AddNote([FromBody] NoteDto note)
    {
        ValidationResult validationResult = await _noteDtoValidator.ValidateAsync(note);

        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse(validationResult.ToDictionary()));
        }

        Note updatedNote = note.ToDomain();

        try
        {
            await _noteService.AddNote(updatedNote);
        }
        catch (Exception exception)
        {
            return BadRequest(new ErrorResponse(exception.Message));
        }
        _unitOfWork.Commit();

        return Ok();
    }

    [HttpGet]
    [Route("notes/{id}")]
    public async Task<IActionResult> GetNote(string id)
    {
        Note receivedNote = await _noteService.GetNote(id);
        try
        {
            return Ok(receivedNote);
        }
        catch (Exception exception)
        {
             return BadRequest(new ErrorResponse(exception.Message));
        }
    }

    [HttpDelete]
    [Route("notes/{id}")]
    public async Task<IActionResult> DeleteNote(string id)
    {
        try
        {
            await _noteService.DeleteNote(id);
        }
        catch (Exception exception)
        {
             return BadRequest(new ErrorResponse(exception.Message));
        }
        _unitOfWork.Commit();
        return Ok();
    }

    [HttpPut]
    [Route("notes/{id}")]
    public async Task<IActionResult> UpdateNote([FromRoute] string id, [FromBody] NoteDto note)
    {
        ValidationResult validationResult = await _noteDtoValidator.ValidateAsync(note);

        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse(validationResult.ToDictionary()));
        }

        Note updatedNote = note.ToDomain();
        updatedNote.Id = id;

        try
        {
            await _noteService.UpdateNote(updatedNote);
        }
        catch (Exception exception)
        {
             return BadRequest(new ErrorResponse(exception.Message));
        }
        _unitOfWork.Commit();

        return Ok();
    }
}