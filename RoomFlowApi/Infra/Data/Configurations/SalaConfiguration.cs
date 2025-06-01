using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomFlowApi.Domain.Entities;

namespace RoomFlowApi.Infra.Data.Configurations
{
    public class SalaConfiguration : IEntityTypeConfiguration<Sala>
    {
        public void Configure(EntityTypeBuilder<Sala> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.NumeroSala)
                .IsRequired();

            builder.Property(p => p.Descricao)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.StatusSala)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(p => p.TipoSala)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(p => p.FlagExibirNumeroSala)
                .IsRequired();

            builder.ToTable("TAB_Sala");
        }
    }
}