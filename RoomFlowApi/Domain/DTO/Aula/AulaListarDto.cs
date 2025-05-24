namespace RoomFlowApi.Domain.DTO.Aula
{
    public class AulaListarDto
    {
        public Guid DisciplinaId { get; set; }
        public required string DisciplinaNome { get; set; }

        public Guid CursoId { get; set; }
        public required string CursoNome { get; set; }

        public Guid SalaId { get; set; }
        public required string SalaDescricao { get; set; }

        public Guid TurmaId { get; set; }
        public required string TurmaDescricao { get; set; }

        public Guid ProfessorId { get; set; }

        public required string ProfessorNome { get; set; }


    }
}
