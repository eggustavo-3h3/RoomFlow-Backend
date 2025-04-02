using RoomFlowApi.Enumerators;

namespace RoomFlowApi.Domain.DTO.Sala
{
    public class SalaAdicionarDTO
    {
        public string Descricao { get; set; } = string.Empty;
        public EnumStatusSala StatusSala { get; set; }
    }
}
