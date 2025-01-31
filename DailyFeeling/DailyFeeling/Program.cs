using DailyFeeling.Database;
using DailyFeeling.Repositories;
using DailyFeeling.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adiciona suporte a controladores
builder.Services.AddControllers();

// Configuração do DbContext para usar MySQL
ConfigureDbContext(builder);

// Adiciona o Swagger para gerar a documentação da API
builder.Services.AddSwaggerGen();

// Registrar o UserRepository
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Registrar o AuthService
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// Verifica conexão com o banco de dados
CheckDatabaseConnection(app);

// Ativa o middleware do Swagger para gerar o documento
app.UseSwagger();

// Ativa o Swagger UI
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    options.RoutePrefix = string.Empty;
});

// Mapeia os controladores
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();

void ConfigureDbContext(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
            ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));
}

void CheckDatabaseConnection(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        dbContext.Database.OpenConnection(); // Abre a conexão
        Console.WriteLine("✅ Conexão com o banco de dados bem-sucedida!");
        dbContext.Database.CloseConnection(); // Fecha a conexão
    }
    catch (Exception ex)
    {
        Console.WriteLine("❌ Falha ao conectar ao banco de dados.");
        Console.WriteLine($"Erro: {ex.Message}");
    }
}