namespace RoomFlowApi.Domain.DTO.Turma
{
    public class TurmaListarDTO
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public Guid CursoId { get; set; }
    }
}
