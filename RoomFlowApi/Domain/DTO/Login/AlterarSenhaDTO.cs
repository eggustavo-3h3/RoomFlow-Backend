namespace RoomFlowApi.Domain.DTO.Login
{
    public class AlterarSenhaDTO
    {
        public string Login { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public string NovaSenha { get; set; } = string.Empty;
        public string ConfirmarNovaSenha { get; set; } = string.Empty;
    }
}
