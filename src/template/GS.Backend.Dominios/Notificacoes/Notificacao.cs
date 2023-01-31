using System;
using Newtonsoft.Json;

namespace GS.Backend.Dominios.Notificacoes
{
    public class Notificacao
    {
        public Notificacao(string codigo, string mensagem)
        {
            Codigo = codigo;
            Mensagem = mensagem;
        }

        [JsonProperty("codigo")]
        public string Codigo { get; }
        [JsonProperty("mensagem")]
        public string Mensagem { get; }
    }
}

