using ImproveU_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImproveU_backend.DatabaseConfiguration.Configuration;

public class ExercicioConfiguration : IEntityTypeConfiguration<Exercicio>
{
    public void Configure(EntityTypeBuilder<Exercicio> builder)
    {
        builder.ToTable("exercicios");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id")
                                   .HasColumnType("int")
                                   .IsRequired()
                                   .ValueGeneratedOnAdd()
                                   .UseIdentityAlwaysColumn();

        builder.Property(e => e.Nome).HasColumnName("nome")
                                     .IsRequired()
                                     .HasMaxLength(100);
        
        builder.Property(f => f.DataCriacao).HasColumnName("data_criacao")
                                            .HasColumnType("TIMESTAMP")
                                            .ValueGeneratedOnAdd()
                                            .HasDefaultValueSql("now()");
        
        builder.Property(f => f.UltimaAlteracao).HasColumnName("ultima_atualizacao")
                                                .HasColumnType("TIMESTAMP");

        //relacionamentos fk na tabela ItemTreino
        builder.HasMany(e => e.ItensTreino)
               .WithOne(t => t.ExercicioARealizar)
               .HasForeignKey(e => e.ExercicioId);    

    }

}
