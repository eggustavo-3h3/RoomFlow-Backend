using FluentValidation;

namespace RoomFlowApi.Domain.DTO.Turma
{
    public class TurmaAdicionarValidatorDTO : AbstractValidator<TurmaAdicionarDTO>
    {
        public TurmaAdicionarValidatorDTO()
        {
            RuleFor(t => t.Descricao).NotEmpty().WithMessage("Descrição da turma é obrigatória");

            RuleFor(t => t.CursoId).NotEmpty().WithMessage("Curso é obrigatório");
        }
    }
}
