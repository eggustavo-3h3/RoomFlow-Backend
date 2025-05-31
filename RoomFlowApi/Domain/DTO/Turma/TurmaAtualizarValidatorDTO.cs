using FluentValidation;

namespace RoomFlowApi.Domain.DTO.Turma
{
    public class TurmaAtualizarValidatorDTO : AbstractValidator<TurmaAtualizarDTO>
    {
        public TurmaAtualizarValidatorDTO()
        {
            RuleFor(t => t.Id).NotEmpty().WithMessage("Turma não existe");

            RuleFor(t => t.Descricao).NotEmpty().WithMessage("Descrição da turma é obrigatória");

            RuleFor(t => t.CursoId).NotEmpty().WithMessage("Curso é obrigatório");
        }
    }
}
