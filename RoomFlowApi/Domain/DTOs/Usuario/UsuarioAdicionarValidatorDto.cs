using FluentValidation;

namespace RoomFlowApi.Domain.DTOs.Usuario
{
    public class UsuarioAdicionarValidatorDto : AbstractValidator<UsuarioAdicionarDto>
    {
        public UsuarioAdicionarValidatorDto()
        {
            RuleFor(u => u.Login)
                .NotEmpty().WithMessage("Email é obrigatório.")
                .EmailAddress().WithMessage("Formato do email inválido.");

            RuleFor(u => u.Senha)
                .NotEmpty().WithMessage("Senha é obrigatória.")
                .MinimumLength(8).WithMessage("A senha deve ter no mínimo 8 caracteres.");

            RuleFor(u => u.Perfil)
                .NotEmpty().WithMessage("Perfil do usuário é obrigatório.")
                .IsInEnum().WithMessage("Perfil de usuário inválido.");
        }
    }
}
