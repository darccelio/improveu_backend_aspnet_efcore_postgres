using ImproveU_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImproveU_backend.DatabaseConfiguration.Configuration;

public class ItemTreinoConfiguration : IEntityTypeConfiguration<ItemTreinoARealizar>
{
    public void Configure(EntityTypeBuilder<ItemTreinoARealizar> builder)
    {
        builder.ToTable("itens_treino");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityAlwaysColumn();
        builder.Property(e => e.CargaEmKg).HasColumnName("carga").HasColumnType("int");
        builder.Property(e => e.Repeticoes).HasColumnName("repeticoes").HasColumnType("int");
        builder.Property(e => e.Series).HasColumnName("series").HasColumnType("int");
        builder.Property(e => e.IntervaloDescanso).HasColumnName("intervalo_descanso").HasColumnType("int");
        builder.Property(e => e.ExercicioId).HasColumnName("exercicio_id").HasColumnType("int").IsRequired();
        
        builder.Property(e => e.TreinoId).HasColumnName("treino_id").HasColumnType("int").IsRequired();
        builder.Property(e => e.FeedbackId).HasColumnName("feedback_id").HasColumnType("int");
        
        builder.Property(e => e.DataCriacao).HasColumnName("data_criacao").HasColumnType("TIMESTAMP").ValueGeneratedOnAdd().HasDefaultValueSql("now()");
        builder.Property(e => e.UltimaAlteracao).HasColumnName("ultima_atualizacao").HasColumnType("TIMESTAMP");

        //relacionamentos fk na tabela Feedback
        builder.HasOne(e => e.Feedback).WithOne(e => e.ItemTreino).HasForeignKey<Feedback>(e => e.ItemTreinoId);



    }
}
