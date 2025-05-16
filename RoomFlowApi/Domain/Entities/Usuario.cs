using RoomFlowApi.Domain.Enumerators;
using RoomFlowApi.Enumerators;

namespace RoomFlowApi.Domain
{
    public class Usuario    
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public EnumPerfilUsuario Perfil { get; set; }

        public EnumStatusUsuario Status { get; set; }
    }    
}
