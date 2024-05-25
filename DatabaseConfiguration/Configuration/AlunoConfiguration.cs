using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ImproveU_backend.Models;

namespace ImproveU_backend.DatabaseConfiguration.Configuration;

public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
{
    public void Configure(EntityTypeBuilder<Aluno> builder)
    {
        builder.ToTable("alunos");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityAlwaysColumn();       
        builder.Property(e => e.PessoaId).HasColumnName("pessoa_id").HasColumnType("int");

        builder.Property(e => e.DataCriacao).HasColumnName("data_criacao").HasColumnType("TIMESTAMP").ValueGeneratedOnAdd().HasDefaultValueSql("now()");
        builder.Property(e => e.UltimaAlteracao).HasColumnName("ultima_atualizacao").HasColumnType("TIMESTAMP");
    }
}