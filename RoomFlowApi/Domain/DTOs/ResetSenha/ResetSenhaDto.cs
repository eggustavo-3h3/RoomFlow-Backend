namespace RoomFlowApi.Domain.DTOs.ResetSenha;

public class ResetSenhaDto
{
    public Guid ChaveResetSenha { get; set; }
    public string NovaSenha { get; set; } = string.Empty;
    public string ConfirmarNovaSenha { get; set; } = string.Empty;
}