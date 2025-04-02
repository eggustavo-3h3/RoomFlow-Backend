using RoomFlowApi.Enumerators;

namespace RoomFlowApi.Domain
{
    public class Sala
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; } = string.Empty;

        public EnumStatusSala statusSala {  get; set; } 
    }
}
