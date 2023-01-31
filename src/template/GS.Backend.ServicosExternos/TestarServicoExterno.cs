using GS.Backend.Dominios.Idiomas;
using GS.Backend.Dominios.Modelos.Entradas;
using GS.Backend.Dominios.Notificacoes;
using GS.Backend.Dominios.ServicosExternos;
using GS.Backend.Dominios.ServicosExternos.Saidas;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;

namespace GS.Backend.ServicosExternos;
public class TestarServicoExterno : ITestarServicoExterno
{
    private readonly IStringLocalizer<UsarIdioma> _idioma;
    private readonly IConfiguration _configs;
    public TestarServicoExterno(IConfiguration configs, IStringLocalizer<UsarIdioma> idioma)
    {
        _configs = configs;
        _idioma = idioma;
    }

    public async Task<TestarSaida> Processar(TestarEntrada entrada)
    {
        return await Task.Run(()=> new TestarSaida {
            Informacoes = new List<string> {
                _idioma["idiomaConfigurado"].Value,
                string.Format("Sua pergunta foi: \"{0}\"", entrada.Pergunta)
            }
        });
    }
}

