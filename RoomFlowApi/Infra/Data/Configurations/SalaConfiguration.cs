﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomFlowApi.Domain;


namespace RoomFlowApi.Configurations
{
    public class SalaConfiguration : IEntityTypeConfiguration<Sala>
    {
        public void Configure(EntityTypeBuilder<Sala> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.StatusSala)     
                .IsRequired()
                .HasConversion<string>();

            builder.Property (p => p.TipoSala)
                .IsRequired()
                .HasConversion<string>();
            

            builder.ToTable("TAB_Sala");
        }
    }
}
