using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomFlowApi.Domain;

namespace RoomFlowApi.Configurations
{
    public class AulaConfiguration : IEntityTypeConfiguration<Aula>
    {
        public void Configure(EntityTypeBuilder<Aula> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Bloco)
                .IsRequired();
            builder.Property(p => p.Sala)
                .IsRequired();
            builder.Property(p => p.DisciplinaId)
                .IsRequired();
            builder.Property(p => p.Turma)
                .IsRequired();
            builder.Property(p => p.Data)
                .IsRequired();
            builder.Property(p => p.professorId)
                .IsRequired();

            builder.ToTable("TAB_Aula");
        }
    }
}