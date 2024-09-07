using Microsoft.EntityFrameworkCore;
using WebAppDesafio.API.Dominio.Models;

namespace WebAppDesafio.API.Infra.Dados;

public class AppDbContext : DbContext
{
    public DbSet<Chamado> Chamados { get; set; } = null!;
    public DbSet<Departamento> Departamentos { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions)
        : base(dbContextOptions) {}

    /// <summary>
    /// Configura o modelo para este contexto
    /// </summary>
    /// <param name="modelBuilder">O construtor sendo usado para construir o modelo para este contexto.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplica configurações do assembly onde as classes de configuração estão localizadas
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Passa o modelBuilder para a classe base
        base.OnModelCreating(modelBuilder);
    }
}