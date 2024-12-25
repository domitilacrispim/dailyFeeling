using DailyFeeling.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do DbContext para usar MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// Testar a conexão com o banco de dados
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        dbContext.Database.OpenConnection(); // Abre a conexão com o banco
        dbContext.Database.CloseConnection(); // Fecha a conexão
        Console.WriteLine("\n\n\n\n\n\n\n\n\nConexão com o banco de dados bem-sucedida!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\n\n\n\n\n\n\n\n\nErro ao conectar ao banco de dados: {ex.Message}");
    }
}

app.Run();

