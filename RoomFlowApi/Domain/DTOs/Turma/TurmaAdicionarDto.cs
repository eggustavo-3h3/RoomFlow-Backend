namespace RoomFlowApi.Domain.DTOs.Turma
{
    public class TurmaAdicionarDto
    {
        public string Descricao { get; set; } = string.Empty;
        public Guid CursoId { get; set; }
    }
}
