using Application.Autenticacao.Boundaries.Usuario;
using Application.Autenticacao.Commands;
using Application.Autenticacao.Dto.Usuario;
using Application.Autenticacao.UseCases;
using Domain.Base.Communication.Mediator;
using Domain.Base.DomainObjects;
using Domain.Base.Messages.CommonMessages.Notifications;
using MediatR;

namespace Application.Autenticacao.Handlers;

public class AutenticaUsuarioCommandHandler :
        IRequestHandler<AutenticaUsuarioCommand, AutenticaUsuarioOutput>
{
    private readonly IAutenticacaoUseCase _autenticacaoUseCase;
    private readonly IMediatorHandler _mediatorHandler;
    public AutenticaUsuarioCommandHandler(IAutenticacaoUseCase autenticacaoUseCase, IMediatorHandler mediatorHandler)
    {
        _autenticacaoUseCase = autenticacaoUseCase;
        _mediatorHandler = mediatorHandler;
    }

    public async Task<AutenticaUsuarioOutput> Handle(AutenticaUsuarioCommand request, CancellationToken cancellationToken)
    {
        if (request.EhValido())
        {
            try
            {
                AutenticaUsuarioInput input = request.Input;
                var identificaDto = new IdentificaDto(input.Matricula, _autenticacaoUseCase.EncryptPassword(input.Senha));
                var autenticado = await _autenticacaoUseCase.AutenticaUsuario(identificaDto);

                if (string.IsNullOrEmpty(autenticado.Nome))
                {
                    await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, "Matrícula ou senha inválidos"));
                }
                else
                {
                    return autenticado;
                }
            }
            catch (DomainException ex)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, ex.Message));
            }
        }
        else
        {
            foreach (var error in request.ValidationResult.Errors)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, error.ErrorMessage));
            }
        }

        return new AutenticaUsuarioOutput();
    }
}
