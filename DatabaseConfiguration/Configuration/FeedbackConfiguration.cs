using ImproveU_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImproveU_backend.DatabaseConfiguration.Configuration;

public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.ToTable("feedbacks");

        builder.HasKey(f => f.Id);
        builder.Property(f => f.Id).HasColumnName("id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityAlwaysColumn();
        builder.Property(f => f.Mensagem).HasColumnName("mensagem").HasColumnType("varchar(255)").IsRequired();
        builder.Property(f => f.Direcao).HasConversion<int>().HasColumnName("direcao").HasColumnType("int").IsRequired();
        builder.Property(f => f.AlunoId).HasColumnName("aluno_id").HasColumnType("int").IsRequired();
        builder.Property(f => f.EdFisicoId).HasColumnName("ed_fisico_id").HasColumnType("int").IsRequired();

        builder.Property(f => f.DataCriacao).HasColumnName("data_criacao").HasColumnType("TIMESTAMP").ValueGeneratedOnAdd().HasDefaultValueSql("now()");
        builder.Property(f => f.UltimaAlteracao).HasColumnName("ultima_atualizacao").HasColumnType("TIMESTAMP");

        //relacionamentos fk na tabela Aluno
        builder.HasOne(f => f.Aluno).WithMany(a => a.Feedbacks).HasForeignKey(f => f.AlunoId);

        //relacionamentos fk na tabela EdFisico
        builder.HasOne(f => f.EdFisico).WithMany(e => e.Feedbacks).HasForeignKey(f => f.EdFisicoId);






        
    }
}