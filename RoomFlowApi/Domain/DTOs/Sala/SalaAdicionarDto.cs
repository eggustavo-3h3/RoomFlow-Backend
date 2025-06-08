using RoomFlowApi.Domain.Enumerators;

namespace RoomFlowApi.Domain.DTOs.Sala
{
    public class SalaAdicionarDto
    {
        public int NumeroSala { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public EnumTipoSala TipoSala { get; set; }
        public bool FlagExibirNumeroSala { get; set; }
    }
}
