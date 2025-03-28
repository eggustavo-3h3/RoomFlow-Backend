namespace RoomFlowApi.Domain.DTO.Aula
{
   using System;

    public class AulaAdicionarDto
    {
        public Guid DisciplinaId { get; set; }
        public Guid SalaId { get; set; }
        public Guid TurmaId { get; set; }
        public DateTime Data { get; set; }
        public Guid professorId { get; set; }
    }
}    