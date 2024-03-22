using Application.Autenticacao.Boundaries.Usuario;
using Application.Autenticacao.Dto.Usuario;
using Application.Autenticacao.UseCases;
using Application.Tests.Autenticacao.Mock.UseCases;
using Moq;

namespace Application.Tests.Autenticacao.UseCases;

public class AutenticacaoUseCaseTests : IDisposable
{
    private readonly Mock<IAutenticacaoUseCase> _useCaseMock;
    private readonly AutenticacaoUseCase _useCase;

    public AutenticacaoUseCaseTests()
    {
        _useCaseMock = MockAutenticacaoUseCase.GetAutencacaoUseCaseMock();
        _useCase = MockAutenticacaoUseCase.GetAutenticacaoUseCase();
    }

    [Fact]
    public async Task AutenticaClienteTest()
    {
        var input = new IdentificaDto("63852797071", "Teste@123");

        _useCaseMock.Setup(u => u.AutenticaUsuario(input)).ReturnsAsync(new AutenticaUsuarioOutput("teste", "tokenAcesso"));

        var resultado = await _useCase.AutenticaUsuario(input);

        //Assert
        var loginRetornado = Assert.IsType<AutenticaUsuarioOutput>(resultado);
        Assert.Equal("teste", loginRetornado.Nome);
        Assert.True(!string.IsNullOrEmpty(loginRetornado.TokenAcesso));
    }

    public void Dispose()
    {
        _useCase.Dispose();
        GC.SuppressFinalize(this);
    }
}
