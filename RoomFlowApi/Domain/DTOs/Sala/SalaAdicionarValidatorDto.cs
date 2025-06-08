using FluentValidation;

namespace RoomFlowApi.Domain.DTOs.Sala
{
    public class SalaAdicionarValidatorDto : AbstractValidator<SalaAdicionarDto>
    {
        public SalaAdicionarValidatorDto() 
        {
            RuleFor(s => s.Descricao)
                .NotEmpty().WithMessage("Descrição é obrigatória.");

            RuleFor(s => s.NumeroSala)
                .NotEmpty().WithMessage("Número da sala é obrigatório.");

            RuleFor(s => s.TipoSala)
                .NotEmpty().WithMessage("Tipo de sala é obrigatório")
                .IsInEnum().WithMessage("Tipo de sala inexistente");
        }
    }
}
