using RoomFlowApi.Enumerators;

namespace RoomFlowApi.Domain.DTO.Curso
{
    public class CursoListarDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public EnumPeriodo Periodo { get; set; }
    }
}
