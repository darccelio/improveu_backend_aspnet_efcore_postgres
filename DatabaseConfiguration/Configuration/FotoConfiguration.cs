using ImproveU_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImproveU_backend.DatabaseConfiguration.Configuration;

internal class FotoConfiguration : IEntityTypeConfiguration<Foto>
{

    public void Configure(EntityTypeBuilder<Foto> builder)
    {
        builder.ToTable("fotos");
        builder.HasKey(f => f.Id);
        builder.Property(f => f.Id).HasColumnName("id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityAlwaysColumn();

        builder.Property(f => f.Path).HasColumnName("path").HasColumnType("varchar(100)").IsRequired();

        //builder.Property(f => f.Origem).HasColumnName("origem").HasColumnType("varchar(100)").IsRequired();

        builder.Property(f => f.Extensão).HasColumnName("extensão").HasColumnType("varchar(5)").IsRequired();

        builder.Property(f => f.PessoaId).HasColumnName("pessoa_id").HasColumnType("int").IsRequired();


        builder.Property(e => e.DataCriacao).HasColumnName("data_criacao").HasColumnType("TIMESTAMP").ValueGeneratedOnAdd().HasDefaultValueSql("now()");
        builder.Property(e => e.UltimaAlteracao).HasColumnName("ultima_atualizacao").HasColumnType("TIMESTAMP");

        //relacionamentos fk na tabela pessoas
        builder.HasOne(f => f.Pessoa).WithMany(p => p.Fotos).HasForeignKey(f => f.PessoaId);
}
    }