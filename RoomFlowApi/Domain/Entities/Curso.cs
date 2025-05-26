using RoomFlowApi.Domain.Enumerators;

namespace RoomFlowApi.Domain.Entities
{
    public class Curso
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public EnumPeriodo Periodo { get; set; }
    }    
}
