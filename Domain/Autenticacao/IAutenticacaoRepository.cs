using Domain.Base.Data;

namespace Domain.Autenticacao;

public interface IAutenticacaoRepository : IRepository<AcessoUsuario>
{
    Task<AcessoUsuario> AutenticaUsuario(AcessoUsuario login);

    void AnonimizaClient(string matricula);
}
