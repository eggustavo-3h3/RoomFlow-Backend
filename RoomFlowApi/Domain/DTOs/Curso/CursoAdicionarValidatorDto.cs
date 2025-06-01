using FluentValidation;

namespace RoomFlowApi.Domain.DTOs.Curso
{
    public class CursoAdicionarValidatorDto : AbstractValidator<CursoAdicionarDto>
    {
        public CursoAdicionarValidatorDto() 
        {
            RuleFor(c => c.Nome).NotEmpty().WithMessage("Nome do curso é obrigatório");

            RuleFor(c => c.Periodo)
                .NotEmpty().WithMessage("Periodo do curso é obrigatório")
                .IsInEnum().WithMessage("Periodo não existe");
        }
    }
}
