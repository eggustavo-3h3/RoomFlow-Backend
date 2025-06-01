using FluentValidation;

namespace RoomFlowApi.Domain.DTOs.Turma
{
    public class TurmaAdicionarValidatorDto : AbstractValidator<TurmaAdicionarDto>
    {
        public TurmaAdicionarValidatorDto()
        {
            RuleFor(t => t.Descricao).NotEmpty().WithMessage("Descrição da turma é obrigatória");

            RuleFor(t => t.CursoId).NotEmpty().WithMessage("Curso é obrigatório");
        }
    }
}
