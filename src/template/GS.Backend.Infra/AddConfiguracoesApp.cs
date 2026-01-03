using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using GS.Backend.Dominios.Notificacoes;
using GS.Backend.Dominios.Middlewares;
using FluentValidation.AspNetCore;
using FluentValidation;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using MediatR;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using GS.Backend.Dominios.Comandos;
using GS.Backend.Dominios.ServicosExternos;
using GS.Backend.ServicosExternos;
using Serilog;

namespace GS.Backend.Infra;
public static class AddConfiguracoesApp
{
    /// <summary>
    /// Inicializacao geral e na devida ordem das dependencias
    /// do App
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder Init(this IApplicationBuilder app)
    {
        return app.UseCors()
        .UseHttpsRedirection()
        .UseRouting()
        .UseAuthorization()
        .AddMiddlewaresCustomizados()
        .UseSwaggerCustomizado()
        .UseHealthCheck()
        .UseEndpoints(endpoints => {
            endpoints.MapControllers();
        })
        .UseConfiguracoesSerilog();
    }
    
    /// <summary>
    /// Adicionar Middlewares
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder AddMiddlewaresCustomizados(this IApplicationBuilder app)
    {
        app.UseMiddleware<TratamentoExcecao>();
        return app;
    }

    /// <summary>
    /// Adicionando endpoint swagger para teste com IU da API
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseSwaggerCustomizado(this IApplicationBuilder app)
    {
        return app.UseSwagger()
        .UseSwaggerUI(options => {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Api - Bff V1");
        });
    }

    /// <summary>
    /// Adicionando healthcheck endpoint
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseHealthCheck(this IApplicationBuilder app)
    {
        return app.UseHealthChecks("/actuator/health");
    }

    /// <summary>
    /// Adiciona configuracao de Serilog
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseConfiguracoesSerilog(this IApplicationBuilder app)
    {
        return app.UseSerilogRequestLogging();
    }
}