using Locadora.Data;
using Microsoft.EntityFrameworkCore;
namespace Locadora
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Configure o DbContext aqui
            builder.Services.AddDbContext<LocadoraContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            app.MapGet("/", () => "API Locadora Rodando!");

            app.Run();
        }
    }
}
