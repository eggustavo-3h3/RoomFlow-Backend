using FluentValidation;

namespace RoomFlowApi.Domain.DTO.Disciplina
{
    public class DisciplinaAdicionarValidatorDTO : AbstractValidator<DisciplinaAdcionarDTO>
    {
        public DisciplinaAdicionarValidatorDTO()
        {
            RuleFor(d => d.Nome).NotEmpty().WithMessage("Descrição da disciplina é obrigatória");
            RuleFor(d => d.Descricao).NotEmpty().WithMessage("Descrição da disciplina é obrigatória");
        }
    }
}
