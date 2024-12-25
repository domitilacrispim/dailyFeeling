using DailyFeeling.Database;
using DailyFeeling.Repositories;
using DailyFeeling.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adiciona suporte a controladores
builder.Services.AddControllers();

// Configuração do DbContext para usar MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

// Adiciona o Swagger para gerar a documentação da API
builder.Services.AddSwaggerGen();

// Registrar o UserRepository
builder.Services.AddScoped<UserRepository>();

// Registrar o AuthService
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

// Ativa o middleware do Swagger para gerar o documento
app.UseSwagger();

// Ativa o Swagger UI
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    options.RoutePrefix = string.Empty; // Garante que o Swagger será acessado na raiz
});

// Mapeia os controladores
app.MapControllers();  // Habilita o mapeamento dos controladores

app.MapGet("/", () => "Hello World!");

app.Run();
