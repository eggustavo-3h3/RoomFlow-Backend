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
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(p => p.Login)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Senha)
                .HasMaxLength(250)
                .IsRequired();
            
            builder.Property(p => p.ChaveResetSenha)
                .IsRequired(false);

            builder.Property(p => p.Perfil)
                .IsRequired();

            builder.Property(p => p.Status)
                .IsRequired();

            builder.ToTable("TAB_Usuario");
        }
    }
}
