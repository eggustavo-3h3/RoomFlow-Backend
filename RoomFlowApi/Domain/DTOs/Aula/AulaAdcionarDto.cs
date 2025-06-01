using RoomFlowApi.Domain.Enumerators;

namespace RoomFlowApi.Domain.DTOs.Aula
{
    public class AulaAdicionarDto
    {
        public Guid DisciplinaId { get; set; } //DisciplinaNome
        public Guid CursoId { get; set; } //CursoNome
        public Guid SalaId { get; set; } //SalaNome
        public Guid TurmaId { get; set; }
        public DateTime Data { get; set; }
        public Guid ProfessorId { get; set; }
        public DayOfWeek DiaSemana { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public EnumBloco Bloco { get; set; }
    }
}
