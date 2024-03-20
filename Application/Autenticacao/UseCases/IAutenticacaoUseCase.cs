using Application.Autenticacao.Boundaries.Usuario;
using Application.Autenticacao.Dto.Usuario;

namespace Application.Autenticacao.UseCases;

public interface IAutenticacaoUseCase : IDisposable
{
    Task<AutenticaUsuarioOutput> AutenticaUsuario(IdentificaDto input);
    string EncryptPassword(string dataToEncrypt);
}
