namespace RoomFlowApi.Domain.DTOs.Email;

public class EnviarEmailResponse
{
    public bool Sucesso { get; set; }
    public string Mensagem { get; set; } = string.Empty;
}