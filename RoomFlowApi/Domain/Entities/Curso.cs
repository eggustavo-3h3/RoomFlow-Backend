using RoomFlowApi.Enumerators;

namespace RoomFlowApi.Domain
{
    public class Curso
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public EnumPeriodo Periodo { get; set; }
    }    
}
