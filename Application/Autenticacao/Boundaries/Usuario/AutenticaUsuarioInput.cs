using Swashbuckle.AspNetCore.Annotations;

namespace Application.Autenticacao.Boundaries.Usuario;

public class AutenticaUsuarioInput
{

    public AutenticaUsuarioInput()
    {
        Matricula = string.Empty;
        Senha = string.Empty;
    }

    public AutenticaUsuarioInput(string matricula, string senha)
    {
        Matricula = matricula;
        Senha = senha;
    }

    [SwaggerSchema(
        Title = "CPF",
        Description = "Preencha com uma matrícula válida",
        Format = "string")]
    public string Matricula { get; set; }

    [SwaggerSchema(
        Title = "Senha",
        Description = "Preencha com a senha",
        Format = "string")]
    public string Senha { get; set; }
}
