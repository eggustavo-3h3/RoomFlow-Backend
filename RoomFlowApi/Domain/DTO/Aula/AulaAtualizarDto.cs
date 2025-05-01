namespace RoomFlowApi.Domain.DTO.Aula
{
    public class AulaAtualizarDto
    {
        public Guid Id { get; set; }
        public Guid DisciplinaId { get; set; }
        public Guid SalaId { get; set; }
        public Guid TurmaId { get; set; }
        public DateTime Data { get; set; }
        public Guid professorId { get; set; }
        public Guid CursoId { get; set; }
    }
}

