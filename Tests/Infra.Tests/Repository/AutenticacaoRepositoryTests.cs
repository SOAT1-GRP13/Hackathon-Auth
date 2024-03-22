using Domain.Autenticacao;
using Domain.Autenticacao.Enums;
using Infra.Autenticacao;
using Infra.Autenticacao.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Infra.Tests.Repository;

public class AutenticacaoRepositoryTests : IDisposable
{
    private readonly Mock<IConfiguration> _mockConfiguration;
    private readonly DbContextOptions<AutenticacaoContext> _options;

    public AutenticacaoRepositoryTests()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");

        _mockConfiguration = new Mock<IConfiguration>();
        _options = new DbContextOptionsBuilder<AutenticacaoContext>()
            .UseInMemoryDatabase(databaseName: "auth")
            .Options;
    }

    #region  AutenticaUsuario
    [Fact]
    public async Task AoAutenticarUsuario_DeveRetornarAcessoUsuario()
    {
        // Povoar o banco de dados em memória
        await using (var context = new AutenticacaoContext(_options, _mockConfiguration.Object))
        {
            context.AcessoUsuario.Add(new AcessoUsuario(
                "90897867",
                "123456",
                "teste@teste.com",
                "José Silva",
                Roles.Usuario
                ));
            await context.SaveChangesAsync();
        }

        // Utilizar o contexto populado para o teste
        await using (var context = new AutenticacaoContext(_options, _mockConfiguration.Object))
        {
            var repository = new AutenticacaoRepository(context);
            var usuario = await repository.AutenticaUsuario(new AcessoUsuario("90897867", "123456"));

            // Assert
            Assert.Equal("90897867", usuario.Matricula);
        }
    }

    [Fact]
    public async Task AoAutenticarUsuario_DeveRetornarVazio_Se_UsuarioInvalido()
    {
        // Povoar o banco de dados em memória
        await using (var context = new AutenticacaoContext(_options, _mockConfiguration.Object))
        {
            context.AcessoUsuario.Add(new AcessoUsuario(
                "90897867",
                "123456",
                "teste@teste.com",
                "José Silva",
                Roles.Usuario
                ));
            await context.SaveChangesAsync();
        }

        // Utilizar o contexto populado para o teste
        await using (var context = new AutenticacaoContext(_options, _mockConfiguration.Object))
        {
            var repository = new AutenticacaoRepository(context);
            var usuario = await repository.AutenticaUsuario(new AcessoUsuario("usuarioInvalido", "teste"));

            // Assert
            Assert.Empty(usuario.Nome);
        }
    }

    #endregion

    [Fact]
    public void Dispose_DeveLiberarRecursos()
    {

        var context = new AutenticacaoContext(_options, _mockConfiguration.Object);
        var repository = new AutenticacaoRepository(context);

        // Act
        repository.Dispose();

        // Assert
        Assert.Throws<ObjectDisposedException>(() => context.Set<AcessoUsuario>().ToList());
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
