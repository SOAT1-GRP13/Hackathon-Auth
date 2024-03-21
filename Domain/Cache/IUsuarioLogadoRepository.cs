namespace Domain.Cache;

public interface IUsuarioLogadoRepository
{
    Task<bool> AddUsuarioLogado(string token);
}
