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

    public async Task DeleteNote(CNote note)
    {
        await _apiRepositories.Delete(note);
    }

    public async Task<CNote> GetNote(string id)
    {
        return await _apiRepositories.Get(id);
    }

    public async Task UpdateNote(string id, CNote note)
    {
        await _apiRepositories.Update(id, note);
    }
}