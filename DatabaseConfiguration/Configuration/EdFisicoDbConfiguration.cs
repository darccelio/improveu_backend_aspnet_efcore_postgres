using ImproveU_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImproveU_backend.DatabaseConfiguration.Configuration;

public class EdFisicoDbConfiguration : IEntityTypeConfiguration<EdFisico>
{
    public void Configure(EntityTypeBuilder<EdFisico> builder)
    {
        builder.ToTable("ed_fisicos");
        
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Id).HasColumnName("id")
                                   .HasColumnType("int")
                                   .IsRequired()
                                   .ValueGeneratedOnAdd()
                                   .UseIdentityAlwaysColumn();

        builder.Property(e => e.RegistroConselho).HasColumnName("registro_conselho")
                                                 .HasColumnType("varchar(20)");

        builder.Property(builder => builder.PessoaId).HasColumnName("pessoa_id")
                                                     .HasColumnType("int");

        builder.Property(e => e.TreinoId).HasColumnName("treino_id")
                                         .HasColumnType("int")
                                         .IsRequired(false);

        builder.Property(e => e.DataCriacao).HasColumnName("data_criacao")
                                            .HasColumnType("TIMESTAMP")
                                            .ValueGeneratedOnAdd()
                                            .HasDefaultValueSql("now()");

        builder.Property(e => e.UltimaAlteracao).HasColumnName("ultima_atualizacao")
                                                .HasColumnType("TIMESTAMP");
    }
}