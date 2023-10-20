using Domain;

namespace Application.Note.Services;

public interface INoteService
{
    public Task AddNote(CNote note);
    public Task DeleteNote(CNote note);
    public Task UpdateNote(string id, CNote note);
    public Task<CNote> GetNote(string id);
}