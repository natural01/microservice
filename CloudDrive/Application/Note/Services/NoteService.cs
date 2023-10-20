using Application.Note.Repositories;
using Domain;

namespace Application.Note.Services;

public class NoteService : INoteService
{
    private readonly INoteRepositories _apiRepositories;

    public NoteService(INoteRepositories apiRepositories)
    {
        _apiRepositories = apiRepositories;
    }

    public async Task AddNote(CNote note)
    {
        await _apiRepositories.Add(note);
    }

    public async Task DeleteNote(string id)
    {
        await _apiRepositories.Delete(id);
    }

    public async Task<CNote> GetNote(string id)
    {
        return await _apiRepositories.Get(id);
    }

    public async Task UpdateNote(CNote note)
    {
        await _apiRepositories.Update(note);
    }
}