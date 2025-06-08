using FluentValidation;

namespace RoomFlowApi.Domain.DTOs.Sala
{
    public class SalaAtualizarValidatorDto : AbstractValidator<SalaAtualizarDto>
    {
        public SalaAtualizarValidatorDto()
        {
            RuleFor(s => s.Id).NotEmpty().WithMessage("Sala não existe");

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
