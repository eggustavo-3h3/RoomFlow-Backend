using RoomFlowApi.Domain.Enumerators;

namespace RoomFlowApi.Domain.DTOs.Curso
{
    public class CursoAdicionarDto
    {
        public string Nome { get; set; } = string.Empty;
        public EnumPeriodo Periodo { get; set; }
    }
}
