using Application.Autenticacao.Boundaries.Usuario;
using Application.Autenticacao.Commands;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.CommonMessages.Notifications;
using HackathonAuth.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace HackathonAuth.Tests.Controllers;

public class UsuarioControllerTests
{
    private readonly ServiceProvider _serviceProvider;
    private readonly IServiceProvider _serviceProvideStartTup;

    public UsuarioControllerTests()
    {
        _serviceProvider = new ServiceCollection()
           .AddScoped<IMediatorHandler, MediatorHandler>()
           .AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>()
           .BuildServiceProvider();

        _serviceProvideStartTup = new TestStartup().ConfigureServices(new ServiceCollection());
    }

    #region Testes unitários do AutenticaUsuario
    [Fact]
    public async Task AoChamarAutenticaCliente_DeveRetornarOK_QuandoAsCredenciasEstiveremCorretas()
    {

        var mediatorHandlerMock = new Mock<IMediatorHandler>();

        var domainNotificationHandler = _serviceProvider.GetRequiredService<INotificationHandler<DomainNotification>>();

        var clienteController = new UsuarioController(domainNotificationHandler, mediatorHandlerMock.Object);

        var loginInput = new AutenticaUsuarioInput(
         "47253197836",
         "Teste@123"
        );

        mediatorHandlerMock.Setup(x => x.PublicarNotificacao(It.IsAny<DomainNotification>()));

        mediatorHandlerMock.Setup(x => x.EnviarComando<AutenticaUsuarioCommand, AutenticaUsuarioOutput>(It.IsAny<AutenticaUsuarioCommand>()))
            .ReturnsAsync(new AutenticaUsuarioOutput("teste", "fdhfjsdhfjksdhfkj"));

        var resultado = await clienteController.AutenticaUsuario(loginInput);

        //Assert
        var objectResult = Assert.IsType<OkObjectResult>(resultado);
        var loginRetornado = Assert.IsType<AutenticaUsuarioOutput>(objectResult.Value);
        Assert.Equal("teste", loginRetornado.Nome);
        Assert.True(!string.IsNullOrEmpty(loginRetornado.TokenAcesso));

        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
    }

    [Fact]
    public async Task AoChamarAutenticaCliente_DeveRetornarBadRequest_AoNaoPreencherOsCamposObrigatorios()
    {
        // Arrange

        var domainNotificationHandler = _serviceProvideStartTup.GetRequiredService<INotificationHandler<DomainNotification>>();
        var mediatorHandler = _serviceProvideStartTup.GetRequiredService<IMediatorHandler>();

        var clienteController = new UsuarioController(domainNotificationHandler, mediatorHandler);

        var loginInput = new AutenticaUsuarioInput();

        //Act
        var resultado = await clienteController.AutenticaUsuario(loginInput);

        // Assert
        var badRequestObjectResult = Assert.IsType<ObjectResult>(resultado);
        var mensagensErro = Assert.IsType<List<string>>(badRequestObjectResult.Value);
        Assert.Contains("Matrícula é obrigatória", mensagensErro);
        Assert.Contains("Senha é obrigatório", mensagensErro);
    }

    [Fact]
    public async Task AoChamarAutenticaCliente_DeveRetornarBadRequest_AoPreencherCredenciaisInvalidas()
    {
        // Arrange
        var mediatorHandlerMock = new Mock<IMediatorHandler>();

        // Obtenha uma instância real de DomainNotificationHandler do contêiner
        var domainNotificationHandler = _serviceProvider.GetRequiredService<INotificationHandler<DomainNotification>>();

        var clienteController = new UsuarioController(domainNotificationHandler, mediatorHandlerMock.Object);

        var loginInput = new AutenticaUsuarioInput("usuarioInvalido", "senhaInvalida");

        mediatorHandlerMock.Setup(x => x.PublicarNotificacao(It.IsAny<DomainNotification>()));

        domainNotificationHandler.Handle(new DomainNotification("Erro", "CPF ou senha inválidos"), CancellationToken.None).Wait();

        mediatorHandlerMock.Setup(x => x.EnviarComando<AutenticaUsuarioCommand, AutenticaUsuarioOutput>(It.IsAny<AutenticaUsuarioCommand>()))
            .ReturnsAsync(new AutenticaUsuarioOutput("", ""));

        var controller = new MockController(domainNotificationHandler, mediatorHandlerMock.Object);



        //Act
        var resultado = await clienteController.AutenticaUsuario(loginInput);
        var operacaoValida = controller.OperacaoValida();
        var mensagensErro = controller.ObterMensagensErro();

        // Assert
        Assert.False(operacaoValida);
        Assert.Contains("CPF ou senha inválidos", mensagensErro);
    }

    [Fact]
    public async Task AoChamarAutenticaCliente_DeveRetornarInternalError_AoOcorrerErroInesperado()
    {
        // Arrange
        var mediatorHandlerMock = new Mock<IMediatorHandler>();

        // Obtenha uma instância real de DomainNotificationHandler do contêiner
        var domainNotificationHandler = _serviceProvider.GetRequiredService<INotificationHandler<DomainNotification>>();

        var clienteController = new UsuarioController(domainNotificationHandler, mediatorHandlerMock.Object);

        var loginInput = new AutenticaUsuarioInput("usuarioInvalido", "senhaInvalida");

        mediatorHandlerMock.Setup(x => x.EnviarComando<AutenticaUsuarioCommand, AutenticaUsuarioOutput>(It.IsAny<AutenticaUsuarioCommand>()))
            .ThrowsAsync(new Exception("Simulando uma exceção"));

        var resultado = await clienteController.AutenticaUsuario(loginInput);

        //Assert
        var objectResult = Assert.IsType<ObjectResult>(resultado);
        Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        Assert.Equal("Erro ao tentar realizar LogIn. Erro: Simulando uma exceção", objectResult.Value);
    }

    #endregion
}
