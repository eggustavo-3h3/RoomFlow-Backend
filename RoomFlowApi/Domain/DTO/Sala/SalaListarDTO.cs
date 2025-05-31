using RoomFlowApi.Domain.Enumerators;

namespace RoomFlowApi.Domain.DTO.Sala
{
    public class SalaListarDTO
    {
        public Guid Id { get; set; }
        public string? Descricao { get; set; } = string.Empty;

        public EnumStatusSala StatusSala { get; set; }

        public EnumTipoSala TipoSala { get; set; }
        public int? NumeroSala { get; set; }

    }
}
