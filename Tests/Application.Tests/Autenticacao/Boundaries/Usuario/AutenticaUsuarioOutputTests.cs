using Application.Autenticacao.Boundaries.Usuario;

namespace Application.Tests.Autenticacao.Boundaries.Usuario;

public class AutenticaUsuarioOutputTests
{
    [Fact]
    public void AtribuirEObterValores_DeveFuncionarCorretamente()
    {
        // Arrange
        var autenticaClienteOutput = new AutenticaUsuarioOutput("teste", "tokenAcesso");

        // Act & Assert
        Assert.Equal("teste", autenticaClienteOutput.Nome);
        Assert.Equal("tokenAcesso", autenticaClienteOutput.TokenAcesso);
    }
}
