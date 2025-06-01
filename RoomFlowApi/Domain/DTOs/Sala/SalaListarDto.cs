using RoomFlowApi.Domain.Enumerators;

namespace RoomFlowApi.Domain.DTOs.Sala
{
    public class SalaListarDto
    {
        public Guid Id { get; set; }
        public int NumeroSala { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public EnumStatusSala StatusSala { get; set; }
        public EnumTipoSala TipoSala { get; set; }
        public bool FlagExibirNumeroSala { get; set; }
    }
}
