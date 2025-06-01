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

            RuleFor(a => a.DiaSemana)
                .NotEmpty().WithMessage("Dia da semana é obrigatório");

            RuleFor(a => a.DataInicio)
                .NotEmpty().WithMessage("Data de início é obrigatória")
                .LessThanOrEqualTo(a => a.DataFim).WithMessage("Data de início não pode ser posterior à data de fim")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Data de início não pode ser no passado");

            RuleFor(a => a.DataFim)
                .NotEmpty().WithMessage("Data de fim é obrigatória")
                .GreaterThanOrEqualTo(a => a.DataInicio).WithMessage("Data de fim não pode ser anterior à data de início")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Data de fim não pode ser no passado");
        }
    }
}
