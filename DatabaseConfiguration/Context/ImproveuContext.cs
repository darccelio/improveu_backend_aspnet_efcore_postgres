using ImproveU_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace ImproveU_backend.DatabaseConfiguration.Configuration;

public class ImproveuContext(DbContextOptions<ImproveuContext> _options) : DbContext(_options)
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<EdFisico> EdFisicos { get; set; }
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Foto> Fotos { get; set; }


    public DbSet<Exercicio> Exercicios { get; set; }
    public DbSet<Treino> Treinos { get; set; }
    public DbSet<ItemTreino> ItensTreinos { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsuarioDbConfiguration());
        modelBuilder.ApplyConfiguration(new PessoaDbConfiguration());
        modelBuilder.ApplyConfiguration(new EdFisicoDbConfiguration());
        modelBuilder.ApplyConfiguration(new AlunoConfiguration());
        modelBuilder.ApplyConfiguration(new FotoConfiguration());

        modelBuilder.ApplyConfiguration(new ExercicioConfiguration());
        modelBuilder.ApplyConfiguration(new ItemTreinoConfiguration());
        modelBuilder.ApplyConfiguration(new TreinoConfiguration());
        modelBuilder.ApplyConfiguration(new FeedbackConfiguration());

    }
}