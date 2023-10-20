using Infrastructure.Foundations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Foundations;
using Infrastructure.Database;
using Application.Note.Repositories;
using Application.Note.Services;
using Infrastructure.Note.Repositories;

namespace Infrastructure;

public static class ConfigureServices
{
    public static void AddDatabaseFoundations(this IServiceCollection services)
    {
        //string connectionString = configuration.GetConnectionString("LocalPostgreSQL");

        services.AddDbContext<AppDbContext>(options => options.UseNpgsql("server=localhost;port=5432;username=postgres;database=test1;password=123"));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
