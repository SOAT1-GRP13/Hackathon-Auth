using Application.Autenticacao.Boundaries.Usuario;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace HackathonAuth.SwaggerExamples;

[ExcludeFromCodeCoverage]
public class IdentificaOutputExamples : IMultipleExamplesProvider<AutenticaUsuarioOutput>
{
    public IEnumerable<SwaggerExample<AutenticaUsuarioOutput>> GetExamples()
    {
        yield return SwaggerExample.Create("Autenticado com sucesso", new AutenticaUsuarioOutput("01438749007", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"));

        yield return SwaggerExample.Create("Falha de Autenticação", new AutenticaUsuarioOutput());
    }
}
