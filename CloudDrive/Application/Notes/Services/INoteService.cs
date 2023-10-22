using Domain;

namespace Application.Notes.Services;

public interface INoteService
{
    public Task AddNote(Note note);
    public Task DeleteNote(string id);
    public Task UpdateNote(Note note);
    public Task<Note> GetNote(string id);
    public Task<List<Note>> GetAll();
}