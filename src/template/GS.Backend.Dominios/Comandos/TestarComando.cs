using System;
using Newtonsoft.Json;
using MediatR;
using GS.Backend.Dominios.Modelos.Resultados;

namespace GS.Backend.Dominios.Comandos
{
    public class TestarComando : IRequest<TestarResultado>
    {
        [JsonProperty("pergunta")]
        public string Pergunta { get; set; }
    }
}

