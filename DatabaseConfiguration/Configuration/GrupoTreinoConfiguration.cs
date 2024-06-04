using ImproveU_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImproveU_backend.DatabaseConfiguration.Configuration;

public class GrupoTreinoConfiguration : IEntityTypeConfiguration<GrupoTreino>
{
    public void Configure(EntityTypeBuilder<GrupoTreino> builder)
    {
        builder.ToTable("grupos_treino");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityAlwaysColumn();
        builder.Property(e => e.Nome).HasColumnName("nome").HasColumnType("varchar(255)").IsRequired();
        builder.Property(e => e.TreinoId).HasColumnName("treino_id").HasColumnType("int").IsRequired();

        builder.Property(e => e.DataCriacao).HasColumnName("data_criacao").HasColumnType("TIMESTAMP").ValueGeneratedOnAdd().HasDefaultValueSql("now()");
        builder.Property(e => e.UltimaAlteracao).HasColumnName("ultima_atualizacao").HasColumnType("TIMESTAMP");
    }
}
