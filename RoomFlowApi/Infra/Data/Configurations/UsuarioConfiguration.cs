using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomFlowApi.Domain.Entities;

namespace RoomFlowApi.Infra.Data.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Senha)
                .IsRequired();
            // Adicionar lógica para valor mínimo de caracteres.

            builder.Property(p => p.ChaveResetSenha)
                .IsRequired(false);

            builder.Property(p => p.Perfil)
                .IsRequired();

            builder.ToTable("TAB_Usuario");
        }
    }
}
