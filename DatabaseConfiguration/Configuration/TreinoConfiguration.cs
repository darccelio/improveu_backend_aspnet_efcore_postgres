using ImproveU_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImproveU_backend.DatabaseConfiguration.Configuration;

public class TreinoConfiguration : IEntityTypeConfiguration<Treino>
{
    public void Configure(EntityTypeBuilder<Treino> builder)
    {
        builder.ToTable("treinos");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityAlwaysColumn();

        builder.Property(e => e.EdFisicoId).HasColumnName("ed_fisico_id").HasColumnType("int").IsRequired();
        builder.Property(e => e.AlunoId).HasColumnName("aluno_id").HasColumnType("int").IsRequired();

        builder.Property(e => e.DataInicioVigencia).HasColumnName("data_inicio_vigencia").HasColumnType("timestamp without time zone");
        builder.Property(e => e.DataFimVigencia).HasColumnName("data_fim_vigencia").HasColumnType("timestamp without time zone");


        builder.Property(e => e.Status).HasColumnName("status").HasColumnType("int").HasDefaultValue("1").IsRequired();

        builder.Property(e => e.DataCriacao).HasColumnName("data_criacao").HasColumnType("timestamp without time zone").ValueGeneratedOnAdd().HasDefaultValueSql("now()");
        builder.Property(e => e.UltimaAlteracao).HasColumnName("ultima_atualizacao").HasColumnType("timestamp without time zone");

        //relacionamentos fk na tabela ed_fisicos
        builder.HasOne(e => e.EdFisico).WithMany(t => t.Treinos ).HasForeignKey(t => t.EdFisicoId);

        //relacionamentos fk na tabela Aluno
        builder.HasOne(e => e.Aluno).WithMany(t => t.Treinos).HasForeignKey(e => e.AlunoId);

        //relacionamentos fk na tabela Item Treino
        builder.HasMany(e => e.ItensTreino).WithOne(t => t.Treino).HasForeignKey(e => e.TreinoId);
    }
}
