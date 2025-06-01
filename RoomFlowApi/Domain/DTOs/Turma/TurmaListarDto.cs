using RoomFlowApi.Domain.DTOs.Curso;

namespace RoomFlowApi.Domain.DTOs.Turma
{
    public class TurmaListarDto
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public CursoListarDto Curso { get; set; } = null!;
    }
}
