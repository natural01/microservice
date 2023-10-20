using Application.Foundations;
using Application.Note.Services;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace CloudDrive.Controllers;

[ApiController]
[Route("api/test")]
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
        _unitOfWork.Commit();

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
            throw new Exception("Can't be found");
        }
    }

    [HttpDelete]
    [Route("delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            await _noteService.DeleteNote(id);
        }
        catch (Exception exception)
        {
            throw new Exception("Can't be deleted");
        }
        _unitOfWork.Commit();
        return Ok();
    }

    [HttpPost]
    [Route("update/node")]
    public async Task<IActionResult> Update([FromBody] CNote note)
    {
        try
        {
            await _noteService.UpdateNote(note);
        }
        catch (Exception exception)
        {
            return BadRequest();
        }
        _unitOfWork.Commit();

        return Ok();
    }
}