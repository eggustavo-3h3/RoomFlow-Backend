using RoomFlowApi.Domain.DTOs.Curso;
using RoomFlowApi.Domain.DTOs.Disciplina;
using RoomFlowApi.Domain.DTOs.Sala;
using RoomFlowApi.Domain.DTOs.Turma;
using RoomFlowApi.Domain.DTOs.Usuario;
using RoomFlowApi.Domain.Enumerators;

namespace RoomFlowApi.Domain.DTOs.Aula
{
    public class AulaListarDto
    {
        public Guid Id { get; set; }
        public EnumBloco Bloco { get; set; }
        public DisciplinaListarDto Disciplina { get; set; } = null!;
        public SalaListarDto Sala { get; set; } = null!;
        public TurmaListarDto Turma { get; set; } = null!;
        public DateTime Data { get; set; }
        public UsuarioListarDto Professor { get; set; } = null!;
        public CursoListarDto Curso { get; set; } = null!;
    }
}
