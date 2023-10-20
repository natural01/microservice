using Domain;

namespace Application.Note.Services;

public interface INoteService
{
    public Task AddNote(CNote note);
    public Task DeleteNote(string id);
    public Task UpdateNote(CNote note);
    public Task<CNote> GetNote(string id);
}