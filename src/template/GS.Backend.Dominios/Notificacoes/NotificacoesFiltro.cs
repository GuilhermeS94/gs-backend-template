using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace GS.Backend.Dominios.Notificacoes
{
    public class NotificacoesFiltro : IAsyncResultFilter
    {
        private readonly NotificacaoCtx _notificacaoCtx;
        public NotificacoesFiltro(NotificacaoCtx notificacaoCtx)
        {
            _notificacaoCtx = notificacaoCtx;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (_notificacaoCtx.TemNotificacoes)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.HttpContext.Response.ContentType = "application/json";

                string notifications = JsonConvert.SerializeObject(_notificacaoCtx.Notificacoes);
                await context.HttpContext.Response.WriteAsync(notifications);

                return;
            }

            await next();
        }
    }
}

