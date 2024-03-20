using Application.Autenticacao.Boundaries.Usuario;
using Application.Autenticacao.Commands;
using Application.Autenticacao.Handlers;
using Application.Autenticacao.UseCases;
using Domain.Autenticacao;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.CommonMessages.Notifications;
using Domain.Cache;
using Infra.Autenticacao;
using Infra.Autenticacao.Repository;
using Infra.Cache;
using MediatR;

namespace HackathonAuth.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            //Domain Notifications 
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            //Autenticacao

            services.AddTransient<IRequestHandler<AutenticaUsuarioCommand, AutenticaUsuarioOutput>, AutenticaUsuarioCommandHandler>();
            services.AddTransient<IAutenticacaoRepository, AutenticacaoRepository>();
            services.AddTransient<IUsuarioLogadoRepository, UsuarioLogadoRepository>();
            services.AddScoped<IAutenticacaoUseCase, AutenticacaoUseCase>();
            services.AddScoped<AutenticacaoContext>();
        }
    }
}
