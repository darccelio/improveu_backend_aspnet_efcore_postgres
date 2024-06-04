using ImproveU_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImproveU_backend.DatabaseConfiguration.Configuration;

public class ItemTreinoConfiguration : IEntityTypeConfiguration<ItemTreino>
{
    public void Configure(EntityTypeBuilder<ItemTreino> builder)
    {
        builder.ToTable("itens_treino");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityAlwaysColumn();
        builder.Property(e => e.CargaEmKg).HasColumnName("carga").HasColumnType("int").IsRequired();
        builder.Property(e => e.Repeticoes).HasColumnName("repeticoes").HasColumnType("int").IsRequired();
        builder.Property(e => e.Series).HasColumnName("series").HasColumnType("int").IsRequired();
        
        builder.Property(e => e.ExercicioId).HasColumnName("exercicio_id").HasColumnType("int").IsRequired();
        
        builder.Property(e => e.GrupoTreinoId).HasColumnName("grupo_treino_id").HasColumnType("int").IsRequired();
        builder.Property(e => e.FeedbackId).HasColumnName("feedback_id").HasColumnType("int");
        
        builder.Property(e => e.DataCriacao).HasColumnName("data_criacao").HasColumnType("TIMESTAMP").ValueGeneratedOnAdd().HasDefaultValueSql("now()");
        builder.Property(e => e.UltimaAlteracao).HasColumnName("ultima_atualizacao").HasColumnType("TIMESTAMP");

        //relacionamentos fk na tabela Items Exercicio
        builder.HasOne(e => e.Exercicio).WithOne(e => e.ItemTreino).HasForeignKey<Exercicio>(e => e.ItemTreinoId);

        //relacionamentos fk na tabela Grupo Treino
        builder.HasOne(e => e.GrupoTreino).WithMany(e => e.ItensTreino).HasForeignKey(e => e.GrupoTreinoId);

        //relacionamentos fk na tabela Feedback
        builder.HasOne(e => e.Feedback).WithOne(e => e.ItemTreino).HasForeignKey<Feedback>(e => e.ItemTreinoId);



    }
}
