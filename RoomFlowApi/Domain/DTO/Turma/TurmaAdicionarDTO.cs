namespace RoomFlowApi.Domain.DTO.Turma
{
    public class TurmaAdicionarDTO
    {
        public string Descricao { get; set; } = string.Empty;
        public Guid CursoId { get; set; }
     
    }
}
