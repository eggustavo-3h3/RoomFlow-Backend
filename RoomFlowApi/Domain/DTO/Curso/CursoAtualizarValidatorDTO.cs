using FluentValidation;

namespace RoomFlowApi.Domain.DTO.Curso
{
    public class CursoAtualizarValidatorDTO : AbstractValidator<CursoAtualizarDTO>
    {
        public CursoAtualizarValidatorDTO() 
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("ID do curso é obrigatório para atualização");

            RuleFor(c => c.Nome).NotEmpty().WithMessage("Nome do curso é obrigatório para atualização");

            RuleFor(c => c.Periodo)
                .NotEmpty().WithMessage("Nome do curso é obrigatório para atualização")
                .IsInEnum().WithMessage("Periodo não existe");
        }
    }
}
