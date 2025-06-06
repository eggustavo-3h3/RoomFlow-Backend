﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomFlowApi.Domain.Entities;

namespace RoomFlowApi.Infra.Data.Configurations
{
    public class AulaConfiguration : IEntityTypeConfiguration<Aula>
    {
        public void Configure(EntityTypeBuilder<Aula> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Bloco)
                .IsRequired();
            builder.Property(p => p.SalaId)
                .IsRequired();
            builder.Property(p => p.DisciplinaId)
                .IsRequired();
            builder.Property(p => p.TurmaId)
                .IsRequired();
            builder.Property(p => p.Data)
                .IsRequired();
            builder.Property(p => p.ProfessorId)
                .IsRequired();
            builder.Property(p => p.CursoId)
                .IsRequired();

            builder.ToTable("TAB_Aula");
        }
    }
}