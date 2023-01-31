using System;
using GS.Backend.Dominios.Idiomas;
using GS.Backend.Dominios.Comandos;
using GS.Backend.Dominios.Modelos.Entradas;
using GS.Backend.Dominios.Modelos.Resultados;
using GS.Backend.Dominios.Notificacoes;
using GS.Backend.Dominios.ServicosExternos;
using MediatR;
using Mapster;
using Microsoft.Extensions.Localization;

namespace GS.Backend.Dominios.Processadores
{
    public class TestarProcessador : IRequestHandler<TestarComando, TestarResultado>
    {
        private readonly ITestarServicoExterno _servicoExterno;
        private readonly NotificacaoCtx _notificacaoCtx;
        private readonly IStringLocalizer<UsarIdioma> _idioma;
        public TestarProcessador(NotificacaoCtx notificacaoCtx, ITestarServicoExterno servicoExterno, IStringLocalizer<UsarIdioma> idioma)
        {
            _notificacaoCtx = notificacaoCtx;
            _servicoExterno = servicoExterno;
            _idioma = idioma;
        }

        public async Task<TestarResultado> Handle(TestarComando request, CancellationToken cancellationToken)
        {
            TestarEntrada entrada = new TestarEntrada(request, _idioma);

            if (entrada.Invalido)
            {
                _notificacaoCtx.AdicionarNotificacoes(entrada.ValidacaoResultado);
                return null;
            }

            var resposta = await _servicoExterno.Processar(entrada);
            return resposta.Adapt<TestarResultado>();
        }
    }
}

