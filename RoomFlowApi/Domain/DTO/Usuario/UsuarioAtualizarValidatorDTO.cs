using FluentValidation;

namespace RoomFlowApi.Domain.DTO.Usuario
{
    public class UsuarioAtualizarValidatorDTO : AbstractValidator<UsuarioAtualizarDTO>
    {
        public UsuarioAtualizarValidatorDTO()
        {
            RuleFor(u => u.Id).NotEmpty().WithMessage("Usuário não existe");

            RuleFor(u => u.Login)
                .NotEmpty().WithMessage("Email é obrigatório.")
                .EmailAddress().WithMessage("Formato do email inválido.");

            RuleFor(u => u.Perfil)
                .NotEmpty().WithMessage("Perfil do usuário é obrigatório.")
                .IsInEnum().WithMessage("Perfil de usuário inválido.");

            RuleFor(u => u.Status)
                .NotEmpty().WithMessage("Status do usuário é obrigatório.")
                .IsInEnum().WithMessage("Status de usuário inválido.");
        }
    }
}
