using FluentValidation;

namespace RoomFlowApi.Domain.DTOs.Turma
{
    public class TurmaAtualizarValidatorDto : AbstractValidator<TurmaAtualizarDto>
    {
        public TurmaAtualizarValidatorDto()
        {
            RuleFor(t => t.Id).NotEmpty().WithMessage("Turma não existe");

            RuleFor(t => t.Descricao).NotEmpty().WithMessage("Descrição da turma é obrigatória");

            RuleFor(t => t.CursoId).NotEmpty().WithMessage("Curso é obrigatório");
        }
    }
}
