using FluentValidation;

namespace RoomFlowApi.Domain.DTOs.AlterarSenha;

public class AlterarSenhaDtoValidator : AbstractValidator<AlterarSenhaDto>
{
    public AlterarSenhaDtoValidator()
    {
        RuleFor(p => p.Senha)
            .NotEmpty().WithMessage("O campo Senha não pode estar vázio");

        RuleFor(p => p.NovaSenha)
            .NotEmpty().WithMessage("O campo Nova Senha não pode estar vazio");
        
        RuleFor(p => p.ConfirmarNovaSenha)
            .Equal(p => p.NovaSenha).WithMessage("As novas senhas não coincidem");
    }
}