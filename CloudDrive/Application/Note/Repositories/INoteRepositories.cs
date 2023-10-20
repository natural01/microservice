using Domain;

namespace Application.Note.Repositories;
public interface INoteRepositories
{
    public Task Add(CNote note);
    public Task Delete(string id);
    public Task Update(CNote note);
    public Task<CNote> Get(string id);
}
