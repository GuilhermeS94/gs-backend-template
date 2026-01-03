using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Templates;

namespace GS.Backend.Infra;
public static class AddConfiguracoesHost
{
    public static IHostBuilder Init(this IHostBuilder host)
    {
        return host.AddAmbientes()
        .AddConfiguracoesSerilog();
    }

    public static IHostBuilder AddAmbientes(this IHostBuilder host)
    {
        return host.ConfigureAppConfiguration((ctx, builder) => {
            var env = ctx.HostingEnvironment.EnvironmentName;

            builder.AddJsonFile("appsettings.json", false, true);
            builder.AddJsonFile($"appsettings.{env}.json", false, true);

            builder.AddEnvironmentVariables();
        });
    }

    public static IHostBuilder AddConfiguracoesSerilog(this IHostBuilder host)
    {
        return host.UseSerilog((ctx, configuracaoLogger, log) => {
            log.ReadFrom.Configuration(ctx.Configuration);
            // .WriteTo.Async(a => a.Console(
            //     new ExpressionTemplate("{ { \"@t\": @t:yyyy-MM-ddTHH:mm:ss, \"@l\": @l, \"@mt\": @mt, ..@p, \"@x\": @x } }\n")
            // ));
        });
    }
}