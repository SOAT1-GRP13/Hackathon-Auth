using Domain.Autenticacao;
using Domain.Base.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Autenticacao.Repository;

public class AutenticacaoRepository : IAutenticacaoRepository
{
    private readonly AutenticacaoContext _context;

    public AutenticacaoRepository(AutenticacaoContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public void AnonimizaClient(string matricula)
    {
        var cliente = _context.AcessoUsuario.Where(x => x.Matricula == matricula).FirstOrDefaultAsync().Result;

        if (cliente is null)
            return;

        cliente.Anonimiza();
    }

    public async Task<AcessoUsuario> AutenticaUsuario(AcessoUsuario login)
    {
        var usuario = await _context.AcessoUsuario.Where(x => x.Matricula == login.Matricula && x.Senha == login.Senha).AsNoTracking().FirstOrDefaultAsync();

        if (usuario is null)
        {
            return new AcessoUsuario();
        }
        else
        {
            return usuario;
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
