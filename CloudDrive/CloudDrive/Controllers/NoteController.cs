﻿using Application.Foundations;
using Application.Notes.Services;
using CloudDrive.Dto;
using Domain;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace CloudDrive.Controllers;

[ApiController]
[Route("api")]
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
            return BadRequest();
        }

        Note updatedNote = note.ToDomain();
        List<Note> notesList = await _noteService.GetAll();
        if (notesList.Count < 1)
        {
            updatedNote.Id = 1.ToString();
        } else
        {
            int lastId = Int32.Parse(notesList[^1].Id) + 1;
            updatedNote.Id = lastId.ToString();
        }

        try
        {
            await _noteService.AddNote(updatedNote);
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
    public async Task<IActionResult> GetNote(string id)
    {
        Note receivedNote = await _noteService.GetNote(id);
        try
        {
            return Ok(receivedNote);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
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
            return BadRequest(exception.Message);
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
            return BadRequest();
        }

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