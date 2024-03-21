namespace Application.Autenticacao.Dto.Usuario;

public class IdentificaDto
{
    #region Construtores
    public IdentificaDto()
    {
        Matricula = string.Empty;
        Senha = string.Empty;
    }
    public IdentificaDto(string matricula, string senha)
    {
        Matricula = matricula.Trim();
        Senha = senha;
    }
    #endregion

    public string Matricula { get; private set; }
    public string Senha { get; private set; }
}
