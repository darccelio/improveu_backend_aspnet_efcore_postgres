using ImproveU_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImproveU_backend.DatabaseConfiguration.Configuration;

public class PessoaDbConfiguration : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder.ToTable("pessoas");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityAlwaysColumn();
        builder.Property(e => e.Nome).HasColumnName("nome").HasColumnType("varchar(255)").IsRequired();
        builder.Property(e => e.Cpf).HasColumnName("cpf").HasColumnType("varchar(11)").IsRequired();
        builder.Property(e => e.UsuarioId).HasColumnName("usuario_id").HasColumnType("int").IsRequired();

        builder.Property(e => e.DataCriacao).HasColumnName("data_criacao").HasColumnType("TIMESTAMP").ValueGeneratedOnAdd().HasDefaultValueSql("now()");
        builder.Property(e => e.UltimaAlteracao).HasColumnName("ultima_atualizacao").HasColumnType("TIMESTAMP");

        //relacionamentos fk na tabela ed_fisicos
        builder.HasOne(e => e.EdFisico).WithOne(p => p.Pessoa).HasForeignKey<EdFisico>(e => e.PessoaId);

        //relacionamentos fk na tabela alunos
        builder.HasOne(e => e.Aluno).WithOne(p => p.Pessoa).HasForeignKey<Aluno>(e => e.PessoaId);
    }

}