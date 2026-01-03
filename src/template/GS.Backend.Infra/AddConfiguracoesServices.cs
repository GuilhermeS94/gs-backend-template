using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using GS.Backend.Dominios.Notificacoes;
using GS.Backend.Dominios.Middlewares;
using FluentValidation;
using MediatR;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using GS.Backend.Dominios.Comandos;
using GS.Backend.Dominios.ServicosExternos;
using GS.Backend.ServicosExternos;
using Serilog;
using Serilog.Templates;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;

namespace GS.Backend.Infra;
public static class AddConfiguracoesServices
{
    public static IServiceCollection Init(this IServiceCollection services, IConfiguration appconfig)
    {
        services.AddIdiomas()
        .AddControllers();
        
        services.AddConfiguracoesCors();

        services.AddMvcCore(options => options.Filters.Add<NotificacoesFiltro>())
        .AddApiExplorer()
        .AddNewtonsoftJson();

        services.AddSwaggerCustomizado()
        .AddOptions();

        services.AddHealthChecks();

        services
        .AddConfiguraoesLogs()
        .AddHttpClient()
        .AddFiltros()
        .AddServicosExternos()
        .AddComandos();

        // services.AddConfiguracoesApi()
        // .AddHttpClient()
        // .AddFiltros()
        // .AddServicosExternos()
        // .AddComandos();

        return services;
    }

    /// <summary>
    /// Adicionar configuracoes de Cross Origin Request (CORS)
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddConfiguracoesCors(this IServiceCollection services)
    {
        services.AddCors(options => {
            options.AddDefaultPolicy(policy => {
                policy.AllowAnyOrigin();
                policy.AllowAnyMethod();
                policy.AllowAnyHeader();
            });
        });

        return services;
    }

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
                                        idiomasDaAplicacao[1].Name : escolherUmIdioma;

                return Task.FromResult<ProviderCultureResult?>(new ProviderCultureResult(idiomaDefault, idiomaDefault));
            }));
        });

        return services;
    }


    /// <summary>
    /// Configurar os logs da aplicacao
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddConfiguraoesLogs(this IServiceCollection services)
    {
        services.AddConfiguraoesSerilog();
        return services;
    }

    public static IServiceCollection AddConfiguraoesSerilog(this IServiceCollection services)
    {
        // Log.Logger = new LoggerConfiguration()
        // // .WriteTo.Console(new JsonFormatter()).CreateLogger();
        // // .WriteTo.Console(new CompactJsonFormatter(
        // //     //"{ {@t, @mt, @l: if @l = 'Information' then undefined() else @l, @x, ..@p} }\n"
        // // ))
        // .WriteTo.Console(new JsonFormatter())
        // //.WriteTo.Console(new CompactJsonFormatter())
        // .CreateLogger();
        //.WriteTo.Console(new RenderedCompactJsonFormatter()).CreateLogger();
        //"formatter": "Serilog.Formatting.Compact.CompactJsonFormatter, Serilog.Formatting.Compact" 
        //"outputTemplate": "[{Timestamp:dd/MM/yyyy HH:mm:ss} {Level:u3} {SourceContext:j} {Message:j} {Properties:j} {NewLine} {Exception}]",
        //"outputTemplate": "{ Momento: {Timestamp:HH:mm:ss}, Urgencia: Level:u3, Body: SourceContext, Mensagem: Message:1, NewLine:1, Exception:1 }",
        //          "formatter": "Serilog.Templates.ExpressionTemplate, Serilog.Expressions",
        // "formatter": {
        //     "type": "Serilog.Templates.ExpressionTemplate, Serilog.Expressions",
        //     "template": "[{@t:dd/MM/yyyy HH:mm:ss} {@l:u3}] {@m}\n{@x}"
        // }

        return services;
    }
}