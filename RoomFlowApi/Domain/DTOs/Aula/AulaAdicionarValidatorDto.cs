using FluentValidation;

namespace RoomFlowApi.Domain.DTOs.Aula
{
    public class AulaAdicionarValidatorDto : AbstractValidator<AulaAdicionarDto>
    {
        public AulaAdicionarValidatorDto() 
        {
            RuleFor(a => a.Bloco)
                .NotEmpty().WithMessage("Preencha o bloco")
                .IsInEnum().WithMessage("Bloco não existe");

            RuleFor(a => a.ProfessorId)
                .NotEmpty().WithMessage("Professor é obrigatório");

            RuleFor(a => a.CursoId)
                .NotEmpty().WithMessage("Curso é obrigatório");

            RuleFor(a => a.SalaId)
                .NotEmpty().WithMessage("Sala é obrigatória");
        }
    }
}
