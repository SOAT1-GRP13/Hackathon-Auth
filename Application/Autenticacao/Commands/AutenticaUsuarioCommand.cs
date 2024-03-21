using Application.Autenticacao.Boundaries.Usuario;
using Application.Autenticacao.Commands.Validation;
using Domain.Base.Messages;

namespace Application.Autenticacao.Commands;

public class AutenticaUsuarioCommand : Command<AutenticaUsuarioOutput>
{
    public AutenticaUsuarioCommand(AutenticaUsuarioInput input)
    {
        Input = input;
    }

    public AutenticaUsuarioInput Input { get; set; }

    public override bool EhValido()
    {
        ValidationResult = new AutenticaUsuarioValidation().Validate(Input);
        return ValidationResult.IsValid;
    }
}
