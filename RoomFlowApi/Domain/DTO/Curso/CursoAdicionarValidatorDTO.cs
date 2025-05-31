using FluentValidation;

namespace RoomFlowApi.Domain.DTO.Curso
{
    public class CursoAdicionarValidatorDTO : AbstractValidator<CursoAdicionarDTO>
    {
        public CursoAdicionarValidatorDTO() 
        {
            RuleFor(c => c.Nome).NotEmpty().WithMessage("Nome do curso é obrigatório");

            RuleFor(c => c.Periodo)
                .NotEmpty().WithMessage("Periodo do curso é obrigatório")
                .IsInEnum().WithMessage("Periodo não existe");
        }
    }
}
