using RoomFlowApi.Domain.Enumerators;

namespace RoomFlowApi.Domain.DTOs.Aula
{
    public class GeradorAulaDto
    {
        public Guid DisciplinaId { get; set; }
        public Guid CursoId { get; set; }
        public Guid SalaId { get; set; } 
        public Guid TurmaId { get; set; }
        public DateTime Data { get; set; }
        public Guid ProfessorId { get; set; }
        public DayOfWeek DiaSemana { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public EnumBloco Bloco { get; set; }
    }
}
