using RoomFlowApi.Domain.Enumerators;

namespace RoomFlowApi.Domain.DTOs.Usuario
{
    public class UsuarioAdicionarDto
    {
        public string Nome { get; set; } = string.Empty;

        public string Login { get; set; } = string.Empty;

        public string Senha { get; set; } = string.Empty;

        public EnumPerfilUsuario Perfil { get; set; }
    }
}
