using RoomFlowApi.Enumerators;

namespace RoomFlowApi.Domain.DTO.Curso
{
    public class CursoAdicionarDTO
    {
        public string Curso { get; set; } = string.Empty;
        public EnumPeriodo Periodo { get; set; }
       

    }
}
