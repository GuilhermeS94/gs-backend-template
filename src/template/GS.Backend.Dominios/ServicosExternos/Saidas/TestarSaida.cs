using System;
using Newtonsoft.Json;

namespace GS.Backend.Dominios.ServicosExternos.Saidas
{
    public class TestarSaida
    {
        [JsonProperty("infos")]
        public IEnumerable<string> Informacoes { get; set; }
    }
}

