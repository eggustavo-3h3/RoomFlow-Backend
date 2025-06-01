using FluentValidation;

namespace RoomFlowApi.Domain.DTOs.Disciplina
{
    public class DisciplinaAdicionarValidatorDto : AbstractValidator<DisciplinaAdicionarDto>
    {
        public DisciplinaAdicionarValidatorDto()
        {
            RuleFor(d => d.Nome).NotEmpty().WithMessage("Descrição da disciplina é obrigatória");
            RuleFor(d => d.Descricao).NotEmpty().WithMessage("Descrição da disciplina é obrigatória");
        }
    }
}
