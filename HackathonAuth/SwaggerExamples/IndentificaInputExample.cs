using Application.Autenticacao.Boundaries.Usuario;
using Swashbuckle.AspNetCore.Filters;

namespace HackathonAuth.SwaggerExamples;

public class IndentificaInputExample : IExamplesProvider<AutenticaUsuarioInput>
{
    public AutenticaUsuarioInput GetExamples()
    {
        return new AutenticaUsuarioInput("01438749007", "Teste@123");
    }
}
