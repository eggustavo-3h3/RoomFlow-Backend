using RoomFlowApi.Domain.Enumerators;

namespace RoomFlowApi.Domain.DTOs.Usuario
{
    public class UsuarioListarDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public EnumPerfilUsuario Perfil { get; set; }
        public EnumStatusUsuario Status { get; set; }
    }
}
