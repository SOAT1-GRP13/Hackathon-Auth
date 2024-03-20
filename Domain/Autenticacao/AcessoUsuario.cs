using Domain.Base.DomainObjects;

namespace Domain.Autenticacao;

public class AcessoUsuario : Entity, IAggregateRoot
{
    #region construtores
    public AcessoUsuario()
    {
        Matricula = string.Empty;
        Senha = string.Empty;
        Nome = string.Empty;
        Email = string.Empty;
    }

    public AcessoUsuario(string matricula, string senha)
    {
        Matricula = matricula;
        Senha = senha;
        Email = string.Empty;
        Nome = string.Empty;

        ValidarAutenticacao();
    }

    public AcessoUsuario(string matricula, string senha, string email, string nome)
    {
        Matricula = matricula;
        Senha = senha;
        Email = email;
        Nome = nome;

        ValidarCadastro();
    }

    #endregion

    public string Matricula { get; private set; }

    public string Senha { get; private set; }

    public string Nome { get; private set; }

    public string Email { get; private set; }


    public void ValidarAutenticacao()
    {
        Validacoes.ValidarSeVazio(Matricula, "O campo Matricula não pode estar vazio");
        Validacoes.ValidarSeVazio(Senha, "O campo Senha não pode estar vazio");
    }

    public void ValidarCadastro()
    {
        Validacoes.ValidarSeVazio(Matricula, "O campo Matricula não pode estar vazio");
        Validacoes.ValidarSeVazio(Senha, "O campo Senha não pode estar vazio");
        Validacoes.ValidarSeVazio(Nome, "O campo Nome não pode estar vazio");
        Validacoes.ValidarSeVazio(Email, "O campo Email não pode estar vazio");
        Validacoes.ValidarEmail(Email, "Email inválido");
    }

    public void Anonimiza()
    {
        Matricula = "00000000000";
        Nome = "Anonimo";
        Email = "Anonimo@anonimo.com.br";
    }
}
