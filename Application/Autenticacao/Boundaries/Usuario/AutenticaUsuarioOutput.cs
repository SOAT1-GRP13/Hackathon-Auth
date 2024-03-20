using Swashbuckle.AspNetCore.Annotations;

namespace Application.Autenticacao.Boundaries.Usuario;

public class AutenticaUsuarioOutput
{
    #region construtores
    public AutenticaUsuarioOutput()
    {
        TokenAcesso = string.Empty;
        Nome = string.Empty;

    }
    public AutenticaUsuarioOutput(string nome, string tokenAcesso)
    {
        Nome = nome;
        TokenAcesso = tokenAcesso;
    }
    #endregion

    [SwaggerSchema(
        Title = "Nome",
        Description = "Nome cadastrado pelo cliente",
        Format = "string")]
    public string Nome { get; set; }

    [SwaggerSchema(
        Title = "TokenAcesso",
        Description = "Token de acesso do cliente identificado",
        Format = "string")]
    public string TokenAcesso { get; set; }
}
