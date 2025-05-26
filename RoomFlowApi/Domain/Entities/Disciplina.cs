namespace RoomFlowApi.Domain.Entities
{
    public class Disciplina
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
    }
    
}