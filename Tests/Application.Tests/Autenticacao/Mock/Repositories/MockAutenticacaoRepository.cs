using Domain.Autenticacao;
using Domain.Autenticacao.Enums;
using Domain.Base.Data;
using Moq;

namespace Application.Tests.Autenticacao.Mock.Repositories;

public static class MockAutenticacaoRepository
{
    public static Mock<IAutenticacaoRepository> GetAutenticacaoRepository()
    {
        var usuario = new AcessoUsuario("63852797071", "Teste@123", "teste@teste.com", "teste", Roles.Usuario);

        var mockRepo = new Mock<IAutenticacaoRepository>();

        mockRepo.Setup(r => r.AutenticaUsuario(It.IsAny<AcessoUsuario>())).ReturnsAsync(usuario);

        var mockUow = new Mock<IUnitOfWork>();
        mockUow.Setup(u => u.Commit()).ReturnsAsync(true);

        mockRepo.SetupGet(r => r.UnitOfWork).Returns(mockUow.Object);

        return mockRepo;
    }
}
