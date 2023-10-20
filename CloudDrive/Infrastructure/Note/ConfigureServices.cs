using Infrastructure.Foundations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Foundations;
using Infrastructure.Database;
using Application.Note.Repositories;
using Application.Note.Services;
using Infrastructure.Note.Repositories;

namespace Infrastructure.Note;

public static class ConfigureServices
{
    public static void AddNoteRepositories(this IServiceCollection services)
    {
        services.AddScoped<INoteRepositories, NoteRepositories>();
    }

    public static void AddNoteService(this IServiceCollection services)
    {
        services.AddScoped<INoteService, NoteService>();
    }
}
