using Locadora.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
namespace Locadora
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Configuração do Banco de Dados
            builder.Services.AddDbContext<LocadoraContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // 2. Adiciona o suporte para Controllers
            builder.Services.AddControllers()
               .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                });

            // 3. Configuração do Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // -----------------------------------------------------

            var app = builder.Build();

            // --- ÁREA DO MIDDLEWARE (Configurações de execução) ---

            // 4. Habilita o Swagger no ambiente de desenvolvimento
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // 5. Redirecionamento HTTPS (opcional, mas boa prática)
            app.UseHttpsRedirection();

            // 6. Mapeia as rotas dos Controllers
            app.MapControllers();

            app.MapGet("/", () => "API Locadora Rodando! Acesse /swagger para testar.");

            app.Run();
        }
    }
}
