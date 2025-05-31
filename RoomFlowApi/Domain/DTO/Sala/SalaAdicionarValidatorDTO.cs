using FluentValidation;

namespace RoomFlowApi.Domain.DTO.Sala
{
    public class SalaAdicionarValidatorDTO : AbstractValidator<SalaAdicionarDTO>
    {
        public SalaAdicionarValidatorDTO() 
        {
            RuleFor(s => s.Descricao)
                .NotEmpty().WithMessage("Descrição é obrigatória se o número da sala não for informado.")
                .When(s => !s.NumeroSala.HasValue | s.NumeroSala.Value <= 0);

            RuleFor(s => s.NumeroSala)
                .NotEmpty().WithMessage("Número da sala é obrigatório se a descrição não for informada.")
                .When(s => string.IsNullOrWhiteSpace(s.Descricao));

            RuleFor(s => s.StatusSala)
                .NotEmpty().WithMessage("Status da sala é obrigatório")
                .IsInEnum().WithMessage("Status inexistente");

            RuleFor(s => s.TipoSala)
                .NotEmpty().WithMessage("Tipo de sala é obrigatório")
                .IsInEnum().WithMessage("Tipo de sala inexistente");
        }
    }
}
