using Application.Autenticacao.Boundaries.Usuario;
using Application.Autenticacao.Commands;
using Application.Autenticacao.Dto.Usuario;
using Application.Autenticacao.Handlers;
using Application.Autenticacao.UseCases;
using Application.Tests.Autenticacao.Mock.UseCases;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.CommonMessages.Notifications;
using Moq;

namespace Application.Tests.Autenticacao.Handlers;

public class AutenticaUsuarioCommandHandlerTests : IDisposable
{
    private readonly Mock<IAutenticacaoUseCase> _useCaseMock;
    private readonly AutenticaUsuarioCommandHandler _handler;
    private readonly Mock<IMediatorHandler> _mediatorHandlerMock;

    public AutenticaUsuarioCommandHandlerTests()
    {
        _useCaseMock = MockAutenticacaoUseCase.GetAutencacaoUseCaseMock();
        _mediatorHandlerMock = new Mock<IMediatorHandler>();
        _handler = new AutenticaUsuarioCommandHandler(_useCaseMock.Object, _mediatorHandlerMock.Object);
    }

    [Fact]
    public async Task HandleAutenticaUsuarioTest_Autenticado()
    {
        AutenticaUsuarioInput input = new AutenticaUsuarioInput("63852797071", "Teste@123");

        _useCaseMock.Setup(u => u.AutenticaUsuario(It.IsAny<IdentificaDto>())).ReturnsAsync(new AutenticaUsuarioOutput("teste", "tokenAcesso"));

        var result = await _handler.Handle(new AutenticaUsuarioCommand(input), CancellationToken.None);

        //Assert
        var loginRetornado = Assert.IsType<AutenticaUsuarioOutput>(result);
        Assert.Equal("teste", loginRetornado.Nome);
        Assert.True(!string.IsNullOrEmpty(loginRetornado.TokenAcesso));
    }

    [Fact]
    public async Task HandleAutenticaUsuarioTest_NaoAutenticado()
    {
        AutenticaUsuarioInput input = new AutenticaUsuarioInput("63852797071", "Teste@123");

        _useCaseMock.Setup(u => u.AutenticaUsuario(It.IsAny<IdentificaDto>())).ReturnsAsync(new AutenticaUsuarioOutput());

        _mediatorHandlerMock.Setup(x => x.PublicarNotificacao(It.IsAny<DomainNotification>()));

        _mediatorHandlerMock.Object.PublicarNotificacao(new DomainNotification("Erro", "CPF ou senha inválidos")).Wait();

        var result = await _handler.Handle(new AutenticaUsuarioCommand(input), CancellationToken.None);

        //Assert
        var loginRetornado = Assert.IsType<AutenticaUsuarioOutput>(result);
        Assert.True(string.IsNullOrEmpty(loginRetornado.Nome));
        Assert.True(string.IsNullOrEmpty(loginRetornado.TokenAcesso));
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
