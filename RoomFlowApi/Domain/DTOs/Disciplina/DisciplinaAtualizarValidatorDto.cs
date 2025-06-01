using FluentValidation;

namespace RoomFlowApi.Domain.DTOs.Disciplina
{
    public class DisciplinaAtualizarValidatorDto : AbstractValidator<DisciplinaAtualizarDto>
    {
        public DisciplinaAtualizarValidatorDto()
        {
            RuleFor(d => d.Id).NotEmpty().WithMessage("Id da disciplina não existe");

            RuleFor(d => d.Nome).NotEmpty().WithMessage("Descrição da disciplina é obrigatória");

            RuleFor(d => d.Descricao).NotEmpty().WithMessage("Descrição da disciplina é obrigatória");

        }
    }
}
