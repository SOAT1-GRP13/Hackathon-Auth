using Application.Autenticacao.Boundaries.Usuario;

namespace Application.Tests.Autenticacao.Boundaries.Usuario;

public class AutenticaUsuarioInputTests
{
    [Fact]
    public void AtribuirEObterValores_DeveFuncionarCorretamente()
    {
        // Arrange
        var autenticaClienteInput = new AutenticaUsuarioInput("63852797071", "teste@123");

        // Act & Assert
        Assert.Equal("63852797071", autenticaClienteInput.Matricula);
    }
}
