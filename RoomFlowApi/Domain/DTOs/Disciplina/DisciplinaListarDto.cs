namespace RoomFlowApi.Domain.DTOs.Disciplina
{
    public class DisciplinaListarDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
    }
}
