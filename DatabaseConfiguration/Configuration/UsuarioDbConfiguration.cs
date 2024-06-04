using ImproveU_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImproveU_backend.DatabaseConfiguration.Configuration;

public class UsuarioDbConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuarios");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityAlwaysColumn();

        builder.Property(e => e.Email).HasColumnName("email").HasColumnType("varchar(255)").IsRequired();
        builder.Property(e => e.Papel).HasColumnName("tipo_pessoa").HasColumnType("int").IsRequired();

        builder.Property(e => e.Senha).HasColumnName("senha").HasColumnType("varchar(255)").IsRequired();
        builder.Property(e => e.Ativo).HasColumnName("ativo").HasColumnType("smallint").IsRequired();

        builder.Property(e => e.DataCriacao).HasColumnName("data_criacao").HasColumnType("TIMESTAMP").ValueGeneratedOnAdd().HasDefaultValueSql("now()");
        builder.Property(e => e.UltimaAlteracao).HasColumnName("ultima_atualizacao").HasColumnType("TIMESTAMP");

        //relacionamentos fk na tabela Pessoa
        builder.HasOne(e => e.Pessoa).WithOne(e => e.Usuario).HasForeignKey<Pessoa>(e => e.UsuarioId);
    }
}
