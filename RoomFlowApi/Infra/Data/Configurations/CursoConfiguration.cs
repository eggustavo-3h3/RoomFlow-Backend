using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomFlowApi.Domain.Entities;

namespace RoomFlowApi.Infra.Data.Configurations
{
    public class CursoConfiguration : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome) 
                .IsRequired() 
                .HasMaxLength(50);

            builder.Property(p => p.Periodo)
                .HasConversion<string>()
                .IsRequired();

            builder.ToTable("TAB_Curso");
        }
    }
}
