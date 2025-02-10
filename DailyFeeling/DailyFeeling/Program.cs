using System.Text;
using DailyFeeling.Database;
using DailyFeeling.Repositories;
using DailyFeeling.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
SetUpJwt(builder);
builder.Services.AddControllers();
ConfigureDbContext(builder);

// Adiciona o Swagger para gerar a documentação da API
builder.Services.AddSwaggerGen(options =>
{
    // Define a segurança Bearer Token
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Entre com o seu token JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Registrar os Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFeelingsRepository, FeelingsRepository>();

// Registrar os Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IFeelingsService, FeelingsService>();

var app = builder.Build();

EnsureDatabaseSetup(app);

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    options.RoutePrefix = string.Empty;
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "Hello World!");
app.Run();

void ConfigureDbContext(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
            ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));
}

void EnsureDatabaseSetup(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        dbContext.Database.OpenConnection(); // Abre a conexão
        dbContext.Database.CloseConnection(); // Fecha a conexão
        Console.WriteLine("✅ Conexão com o banco de dados bem-sucedida!");

        dbContext.Database.Migrate(); // Aplica as migrações
        Console.WriteLine("✅ Migrações aplicadas com sucesso.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("❌ Falha ao conectar ao banco de dados ou executar as migrações.");
        Console.WriteLine($"Erro: {ex.Message}");
    }
}

void SetUpJwt(WebApplicationBuilder builder)
{
    var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:SecretKey"]);

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

    builder.Services.AddAuthorization();
    builder.Services.AddScoped<JwtService>();
}