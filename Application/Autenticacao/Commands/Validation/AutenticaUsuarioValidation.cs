using Application.Autenticacao.Boundaries.Usuario;
using FluentValidation;

namespace Application.Autenticacao.Commands.Validation;

public class AutenticaUsuarioValidation : AbstractValidator<AutenticaUsuarioInput>
{
    public AutenticaUsuarioValidation()
    {
        RuleFor(c => c.Matricula)
            .NotEmpty()
            .WithMessage("Matrícula é obrigatória");

        RuleFor(s => s.Senha)
            .NotEmpty()
            .WithMessage("Senha é obrigatório");
    }
}
