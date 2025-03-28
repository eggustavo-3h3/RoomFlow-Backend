namespace RoomFlowApi.Domain.Base
{
    public class BaseResponse(string mensagem)
    {
        public string Mensagem { get; } = mensagem;
    }
}
