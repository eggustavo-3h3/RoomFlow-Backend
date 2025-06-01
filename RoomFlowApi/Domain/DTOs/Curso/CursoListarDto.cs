using RoomFlowApi.Domain.Enumerators;

namespace RoomFlowApi.Domain.DTOs.Curso
{
    public class CursoListarDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public EnumPeriodo Periodo { get; set; }
    }
}
