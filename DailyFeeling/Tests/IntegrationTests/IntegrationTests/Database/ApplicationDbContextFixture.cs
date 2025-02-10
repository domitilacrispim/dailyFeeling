using System.Data;
using DailyFeeling.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace IntegrationTests.Database;

public class ApplicationDbContextFixture : IDisposable
{
    public ApplicationDbContext DbContext { get; private set; }
    public readonly string? _connectionString;

    public ApplicationDbContextFixture()
    {
        // Carregar o arquivo appsettings.Test.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.IntegrationTest.json", optional: false, reloadOnChange: true)
            .Build();

        _connectionString = configuration.GetConnectionString("DefaultConnection");

        // Detectar automaticamente a versão do MySQL usando a connection string
        var serverVersion = ServerVersion.AutoDetect(_connectionString);

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseMySql(_connectionString, serverVersion) // Usando a versão detectada automaticamente
            .Options;

        DbContext = new ApplicationDbContext(options);

        // Testar se o banco de dados está em execução
        if (!IsDatabaseUp(_connectionString))
        {
            throw new InvalidOperationException("Não foi possível conectar ao banco de dados.");
        }

        // Garantir migrações e limpeza antes dos testes
        DbContext.Database.EnsureDeleted();
        DbContext.Database.Migrate();
    }
    
    [Fact]
    public void Database_Connection_Should_Be_Active()
    {
        // Tentar abrir uma conexão com o banco de dados
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            
            // Verificar se a conexão foi aberta com sucesso
            Assert.Equal(ConnectionState.Open, connection.State);
        }
    }

    private bool IsDatabaseUp(string connectionString)
    {
        try
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection.State == ConnectionState.Open;
        }
        catch
        {
            return false;
        }
    }

    public void Dispose()
    {
        // Limpar recursos
        DbContext.Database.EnsureDeleted();
        DbContext.Dispose();
    }
}