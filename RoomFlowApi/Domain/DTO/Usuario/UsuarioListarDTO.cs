using RoomFlowApi.Domain.Enumerators;

namespace RoomFlowApi.Domain.DTO.Usuario
{
    public class UsuarioListarDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public string Login { get; set; } = string.Empty;

        public string Senha { get; set; } = string.Empty;

        public EnumPerfilUsuario Perfil { get; set; }

        public EnumStatusUsuario Status { get; set; }
    }
}
