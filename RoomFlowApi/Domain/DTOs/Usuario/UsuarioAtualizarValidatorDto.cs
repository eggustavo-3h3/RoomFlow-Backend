using FluentValidation;

namespace RoomFlowApi.Domain.DTOs.Usuario
{
    public class UsuarioAtualizarValidatorDto : AbstractValidator<UsuarioAtualizarDto>
    {
        public UsuarioAtualizarValidatorDto()
        {
            RuleFor(u => u.Id).NotEmpty().WithMessage("Usuário não existe");

            RuleFor(u => u.Login)
                .NotEmpty().WithMessage("Email é obrigatório.")
                .EmailAddress().WithMessage("Formato do email inválido.");

            RuleFor(u => u.Perfil)
                .NotEmpty().WithMessage("Perfil do usuário é obrigatório.")
                .IsInEnum().WithMessage("Perfil de usuário inválido.");
        }
    }
}
