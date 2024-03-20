using Application.Autenticacao.Boundaries.Usuario;
using Application.Autenticacao.Commands;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HackathonAuth.Controllers;

[ApiController]
[Route("[Controller]")]
[SwaggerTag("Endpoints relacionados ao usuário, sendo necessário se autenticar")]
public class UsuarioController : ControllerBase
{
    private readonly IMediatorHandler _mediatorHandler;

    public UsuarioController(INotificationHandler<DomainNotification> notifications,
        IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
    {
        _mediatorHandler = mediatorHandler;
    }

    [HttpPost]
    [Route("AutenticaUsuario")]
    [SwaggerOperation(
        Summary = "Identificação do usuário",
        Description = "Endpoint responsavel por autenticar o usuário")]
    [SwaggerResponse(200, "Retorna dados se autenticado ou não", typeof(AutenticaUsuarioOutput))]
    [SwaggerResponse(500, "Caso algo inesperado aconteça")]
    public async Task<IActionResult> AutenticaUsuario([FromBody] AutenticaUsuarioInput input)
    {
        try
        {
            var command = new AutenticaUsuarioCommand(input);
            var autenticado = await _mediatorHandler.EnviarComando<AutenticaUsuarioCommand, AutenticaUsuarioOutput>(command);

            if (OperacaoValida())
            {
                return Ok(autenticado);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, ObterMensagensErro());
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                      $"Erro ao tentar realizar LogIn. Erro: {ex.Message}");
        }
    }
}
