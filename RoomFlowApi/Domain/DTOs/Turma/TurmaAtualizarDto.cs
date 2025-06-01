namespace RoomFlowApi.Domain.DTOs.Turma
{
    public class TurmaAtualizarDto
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public Guid CursoId { get; set; }
    }
}
