using DailyFeeling.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IntegrationTests.Database;

public class ApplicationDbContextFixture : IDisposable
{
    public ApplicationDbContext DbContext { get; private set; }
    
    public ApplicationDbContextFixture()
    {
        // Carregar o arquivo appsettings.Test.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.IntegrationTest.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 27)))
            .Options;

        DbContext = new ApplicationDbContext(options);

        // Garantir migrações e limpeza antes dos testes
        DbContext.Database.EnsureDeleted();
        DbContext.Database.Migrate();
    }
    
    [Fact]
    public void Configuration_Should_Load_Test_ConnectionString()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.IntegrationTest.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        Assert.Equal("Server=127.0.0.1;Port=3307;Database=testdb;User=testuser;Password=userpassword;", connectionString);
    }

    public void Dispose()
    {
        // Limpar recursos
        DbContext.Database.EnsureDeleted();
        DbContext.Dispose();
    }
}