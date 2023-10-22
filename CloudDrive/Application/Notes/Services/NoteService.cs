using Application.Notes.Repositories;
using Domain;

namespace Application.Notes.Services;

public class NoteService : INoteService
{
    private readonly INoteRepositories _apiRepositories;

    public NoteService(INoteRepositories apiRepositories)
    {
        _apiRepositories = apiRepositories;
    }

    public async Task AddNote(Note note)
    {
        await _apiRepositories.Add(note);
    }

    public async Task DeleteNote(string id)
    {
        await _apiRepositories.Delete(id);
    }

    public async Task<Note> GetNote(string id)
    {
        return await _apiRepositories.Get(id);
    }

    public async Task UpdateNote(Note note)
    {
        await _apiRepositories.Update(note);
    }

    public async Task<List<Note>> GetAll()
    {
        return await _apiRepositories.GetAll();         
    }
}