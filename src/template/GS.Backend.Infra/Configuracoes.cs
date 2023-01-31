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

namespace GS.Backend.Infra;
public static class Configuracoes
{
    /// <summary>
    /// Adicionar endpoint e documentacao de swagger
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSwaggerCustomizado(this IServiceCollection services)
    {
        services.AddSwaggerGen(options => {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "GS Backend Modelo",
                Version = "1",
                Description = "Arquitetura modelo de projeto backend."
            });
        });

        return services;
    }

    /// <summary>
    /// Adicionar configuracoes de
    /// gerencia de JWT token
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddConfiguracoesJWT(this IServiceCollection services, IConfiguration configuration)
    {
        ///TODO: Setup para geracao e/ou validacao de token
        return services;
    }

    /// <summary>
    /// Adicionar contexto de banco de dados
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddDbContexto(this IServiceCollection services, IConfiguration configuration)
    {
        ///TODO: Adicionar contexto de banco de dados,
        ///entity, nhibernate...
        return services;
    }

    /// <summary>
    /// Adicionar filtros
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddFiltros(this IServiceCollection services)
    {
        services.AddScoped<NotificacaoCtx>();

        return services;
    }

    /// <summary>
    /// Adicionar commandos de Command Pattern,
    /// normalmente objetos de entrada da aplicacao
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddComandos(this IServiceCollection services)
    {
        List<Assembly> lista = new List<Assembly>() {
                typeof(TestarComando).Assembly,
            };

        services.AddMvcCore().AddApiExplorer()
        .AddJsonOptions(opts =>
        {
            opts.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseNamingPolicy();
        })
        .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
        .AddDataAnnotationsLocalization();
        services.AddValidatorsFromAssemblies(lista).AddMediatR(lista.ToArray());

        return services;
    }

    /// <summary>
    /// Adicionar servicos externos da aplicacao,
    /// normalmente as classes que executam as operacoes
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddServicosExternos(this IServiceCollection services)
    {
        services.AddScoped<ITestarServicoExterno, TestarServicoExterno>();

        return services;
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
    /// Configurar os idiomas da aplicacao
    /// e setar um default
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddIdiomas(this IServiceCollection services)
    {
        services.AddLocalization();
        services.Configure<RequestLocalizationOptions>(opcoes => {
            CultureInfo[] idiomasDaAplicacao = new[] {
                new CultureInfo("en-US"),
                new CultureInfo("pt-BR"),
                new CultureInfo("es-ES")
            };

            opcoes.SupportedCultures = idiomasDaAplicacao;
            opcoes.SupportedUICultures = idiomasDaAplicacao;

            opcoes.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(ctx => {
                string idiomaRequest = ctx.Request.Headers["Accept-Language"].ToString();
                string escolherUmIdioma = idiomaRequest.Split(',').FirstOrDefault();
                string idiomaDefault = (string.IsNullOrEmpty(escolherUmIdioma) ||
                                        !idiomasDaAplicacao.Contains(new CultureInfo(escolherUmIdioma))) ?
                                        "pt-BR" : escolherUmIdioma;

                return Task.FromResult<ProviderCultureResult?>(new ProviderCultureResult(idiomaDefault, idiomaDefault));
            }));
        });

        return services;
    }
}