using FluentValidation;

namespace RoomFlowApi.Domain.DTOs.Signup
{
    public class SignupDtoValidator : AbstractValidator<SignupDto>
    {
        public SignupDtoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .MaximumLength(150).WithMessage("Nome deve ter no máximo 150 caracteres.");

            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("Login é obrigatório.")
                .MaximumLength(100).WithMessage("Login deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("Senha é obrigatória.")
                .MinimumLength(8).WithMessage("A senha deve ter no mínimo 8 caracteres.");

            RuleFor(x => x.ConfirmacaoSenha)
                .Equal(x => x.Senha).WithMessage("As senhas não coincidem.");
        }
    }
}
