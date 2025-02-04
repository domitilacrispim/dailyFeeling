using System;
using DailyFeeling.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyFeeling.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Feeling> Feelings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ConfigureUsers(modelBuilder);
        ConfigureFeelings(modelBuilder);
    }
    
    private void ConfigureUsers(ModelBuilder modelBuilder)
    {
        // Configuração para a tabela "Users"
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users"); // Definindo o nome da tabela como "Users"

            // Garantir que o Email seja único
            entity.HasIndex(u => u.Email).IsUnique();

            // Garante que o Username será único
            entity.HasIndex(u => u.Username).IsUnique();

            // Configurar o CreatedAt com valor default
            entity.Property(u => u.CreatedAt)
                .HasColumnType("datetime(6)")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
    }

    private void ConfigureFeelings(ModelBuilder modelBuilder)
    {
        // Configuração para a tabela "Feelings"
        modelBuilder.Entity<Feeling>(entity =>
        {
            entity.ToTable("Feelings"); // Nome da tabela no banco

            entity.Property(f => f.Date)
                .HasColumnType("DATE") // Apenas a data, sem hora/minuto
                .IsRequired(); 

            entity.Property(f => f.EmojiUnicode)
                .IsRequired(); 

            entity.HasIndex(f => new { f.UserId, f.Date })
                .IsUnique(); // Garante que um usuário só pode ter um sentimento por dia
        });
    }

}