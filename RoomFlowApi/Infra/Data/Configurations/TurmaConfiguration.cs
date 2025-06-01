using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomFlowApi.Domain.Entities;

namespace RoomFlowApi.Infra.Data.Configurations
{
    public class TurmaConfiguration : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder) {

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Descricao)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.CursoId)
                .IsRequired();

            builder.ToTable("TAB_Turma");
        }
    }
}
