using FluentValidation;

namespace RoomFlowApi.Domain.DTOs.ResetSenha;

public class ResetSenhaDtoValidator : AbstractValidator<ResetSenhaDto>
{
    public ResetSenhaDtoValidator()
    {
        RuleFor(p => p.ChaveResetSenha)
            .NotEmpty().WithMessage("O campo Chave Reset Senha não pode estar vázio");

        RuleFor(p => p.NovaSenha)
            .NotEmpty().WithMessage("O campo Nova Senha não pode estar vazio");

        RuleFor(p => p.ConfirmarNovaSenha)
            .Equal(p => p.NovaSenha).WithMessage("As novas senhas não coincidem");
    }
}