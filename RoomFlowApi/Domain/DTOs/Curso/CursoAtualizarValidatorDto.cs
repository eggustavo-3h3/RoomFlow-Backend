using FluentValidation;

namespace RoomFlowApi.Domain.DTOs.Curso
{
    public class CursoAtualizarValidatorDto : AbstractValidator<CursoAtualizarDto>
    {
        public CursoAtualizarValidatorDto() 
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("ID do curso é obrigatório para atualização");

            RuleFor(c => c.Nome).NotEmpty().WithMessage("Nome do curso é obrigatório para atualização");

            RuleFor(c => c.Periodo)
                .NotEmpty().WithMessage("Nome do curso é obrigatório para atualização")
                .IsInEnum().WithMessage("Periodo não existe");
        }
    }
}
