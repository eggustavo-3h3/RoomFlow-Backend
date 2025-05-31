using FluentValidation;

namespace RoomFlowApi.Domain.DTO.Usuario
{
    public class UsuarioAdicionarValidatorDTO : AbstractValidator<UsuarioAdicionarDTO>
    {
        public UsuarioAdicionarValidatorDTO()
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

            RuleFor(u => u.Status)
                .NotEmpty().WithMessage("Status do usuário é obrigatório.")
                .IsInEnum().WithMessage("Status de usuário inválido.");
        }
    }
}
