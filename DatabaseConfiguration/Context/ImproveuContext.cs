using ImproveU_backend.DatabaseConfiguration.Configuration;
using ImproveU_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace ImproveU_backend.DatabaseConfiguration.Context;

public class ImproveuContext(DbContextOptions<ImproveuContext> _options ) : DbContext(_options)
{
    public DbSet<Usuario> Usuarios { get; set; }

    public DbSet<Pessoa> Pessoas { get; set; }

    public DbSet<EdFisico> EdFisicos { get; set; }
    public DbSet<Aluno> Alunos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsuarioDbConfiguration());
        modelBuilder.ApplyConfiguration(new PessoaDbConfiguration());
        modelBuilder.ApplyConfiguration(new EdFisicoDbConfiguration());
        modelBuilder.ApplyConfiguration(new AlunoConfiguration());
    }
}