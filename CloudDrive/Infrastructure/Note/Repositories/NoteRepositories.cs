using Application.Note.Repositories;
using Domain;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Note.Repositories;

public class NoteRepositories : INoteRepositories
{
    private readonly DbSet<CNote> _notes;

    public NoteRepositories(AppDbContext context)
    {
        _notes = context.Set<CNote>();
    }

    public async Task Add(CNote note)
    {
        await _notes.AddAsync(note);
    }

    public async Task Delete(string id)
    {
        _notes.Remove(await this.Get(id));
    }

    public async Task<CNote> Get(string id)
    {
        return await _notes.FindAsync(id);
    }

    public async Task<List<CNote>> GetAll()
    {
        return await _notes.ToListAsync();
    }

    public async Task Update(CNote note)
    {
        await this.Delete(note.Id);
        await this.Add(note);
    }
}