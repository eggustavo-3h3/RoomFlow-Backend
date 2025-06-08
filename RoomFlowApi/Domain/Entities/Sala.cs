using RoomFlowApi.Domain.Enumerators;

namespace RoomFlowApi.Domain.Entities
{
    public class Sala
    {
        public Guid Id { get; set; }
        public int NumeroSala { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public EnumTipoSala TipoSala { get; set; }
        public bool FlagExibirNumeroSala { get; set; }
    }
}
