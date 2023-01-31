using System;
using System.Net;
using FluentValidation;
using GS.Backend.Dominios.Excecoes;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace GS.Backend.Dominios.Middlewares
{
    public class TratamentoExcecao
    {
        private const string CONTENT_TYPE_APP_JSON = "application/json";
        private readonly RequestDelegate request;
        public TratamentoExcecao(RequestDelegate next)
        {
            this.request = next;
        }

        public Task Invoke(HttpContext ctx) => this.InvokeAsync(ctx);

        async Task InvokeAsync(HttpContext ctx)
        {
            try
            {
                await this.request(ctx);
            }
            catch (Exception ex)
            {
                var response = ctx.Response;
                response.ContentType = CONTENT_TYPE_APP_JSON;

                switch (ex)
                {
                    case ValidationException vex:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonConvert.SerializeObject(new ExcecaoGlobal
                {
                    Codigo = response.StatusCode,
                    Mensagem = ex.Message
                });

                await response.WriteAsync(result);
            }
        }
    }
}

