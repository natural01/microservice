using FluentValidation;
using CloudDrive.Dto;
using Infrastructure;
using Infrastructure.Notes;
using Microsoft.Extensions.Configuration;
using CloudDrive.Authentication;
using Microsoft.OpenApi.Models;

namespace CloudDrive
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "The Api Key to access the Api",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Name = "x-api-key",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Scheme = "ApiKeyScheme"
                });

                var scheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "ApiKey"
                    },
                    In = ParameterLocation.Header,
                };

                var requirement = new OpenApiSecurityRequirement
                {
                    { scheme, new List<string>() }
                };

                c.AddSecurityRequirement(requirement);
            });

            builder.Services.AddScoped<ApiKeyAuthFilter>();

            builder.Services.AddDatabaseFoundations(builder.Configuration);
            builder.Services.AddNoteRepositories();
            builder.Services.AddNoteService();
            builder.Services.AddValidatorsFromAssemblyContaining<NoteDto>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = "";
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}