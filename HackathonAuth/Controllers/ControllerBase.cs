using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HackathonAuth.Controllers;

public abstract class ControllerBase : Controller
{
    private readonly DomainNotificationHandler _notifications;
    private readonly IMediatorHandler _mediatorHandler;

    protected ControllerBase(INotificationHandler<DomainNotification> notifications,
                             IMediatorHandler mediatorHandler)
    {
        _notifications = (DomainNotificationHandler)notifications;
        _mediatorHandler = mediatorHandler;
    }

    protected bool OperacaoValida()
    {
        return !_notifications.TemNotificacao(); // se tem alguma notificacao de problema, retorna operacao invalida.
    }

    protected IEnumerable<string> ObterMensagensErro()
    {
        return _notifications.ObterNotificacoes().Select(c => c.Value).ToList();
    }

    protected void NotificarErro(string codigo, string mensagem)
    {
        _mediatorHandler.PublicarNotificacao(new DomainNotification(codigo, mensagem));
    }

    protected Guid ObterClienteId()
    {
        var identifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!string.IsNullOrEmpty(identifier))
            return Guid.Parse(identifier);

        throw new Exception("Usuário não identificado");
    }
}
