using System;
using DailyFeeling.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyFeeling.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
                      .HasDefaultValueSql("CURRENT_TIMESTAMP"); // Definir o valor default para "CreatedAt"
            });
        }

    }
}

