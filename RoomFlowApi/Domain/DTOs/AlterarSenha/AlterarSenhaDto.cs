namespace RoomFlowApi.Domain.DTOs.AlterarSenha;

public class AlterarSenhaDto
{
    public string Senha { get; set; } = string.Empty;
    public string NovaSenha { get; set; } = string.Empty;
    public string ConfirmarNovaSenha { get; set; } = string.Empty;
}