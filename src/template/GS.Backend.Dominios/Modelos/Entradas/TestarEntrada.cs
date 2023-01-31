using System;
using GS.Backend.Dominios.Idiomas;
using GS.Backend.Dominios.Comandos;
using GS.Backend.Dominios.Validacoes;
using Microsoft.Extensions.Localization;

namespace GS.Backend.Dominios.Modelos.Entradas
{
    public class TestarEntrada : BaseValidacao
    {
        public string Pergunta { get; set; }
        public TestarEntrada(TestarComando comando, IStringLocalizer<UsarIdioma> usarIdioma)
        {
            Pergunta = comando.Pergunta;
            Validar(this, new TestarValidacoes(usarIdioma));
        }
    }
}

