using Domain;

namespace Application.Note.Repositories;
public interface INoteRepositories
{
    public Task Add(CNote note);
    public Task Delete(CNote note);
    public Task Update(string id, CNote note);
    public Task<CNote> Get(string id);
}
