
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using trabalho_biblioteca.Data;
using trabalho_biblioteca.Interfaces;
using trabalho_biblioteca.Repositories;
using trabalho_biblioteca.Services;

namespace trabalho_biblioteca
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();


            builder.Services.AddControllers()
                .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            builder.Services.AddScoped<ILivroService, LivroService>();
            builder.Services.AddScoped<ILivroRepository, LivroRepository>();

            builder.Services.AddDbContext<ContextoBanco>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
