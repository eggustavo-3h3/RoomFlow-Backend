using RoomFlowApi.Domain.Enumerators;

namespace RoomFlowApi.Domain.Entities
{
    public class Usuario    
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public Guid? ChaveResetSenha { get; set; }
        public EnumPerfilUsuario Perfil { get; set; }

        public EnumStatusUsuario Status { get; set; }
    }    
}
