using System;
using GS.Backend.Dominios.Modelos.Entradas;
using Microsoft.Extensions.Localization;
using FluentValidation;
using GS.Backend.Dominios.Idiomas;

namespace GS.Backend.Dominios.Validacoes
{
    public class TestarValidacoes : AbstractValidator<TestarEntrada>
    {
        public TestarValidacoes(IStringLocalizer<UsarIdioma> usarIdioma)
        {
            RuleFor(e => e.Pergunta)
                .NotNull()
                .WithMessage(usarIdioma["msgObrigatorio"].Value)
                .NotEmpty()
                .WithMessage(usarIdioma["msgObrigatorio"].Value)
                .MinimumLength(5)
                .WithMessage(usarIdioma["msgCurta"].Value)
                .MaximumLength(100)
                .WithMessage(usarIdioma["msgLonga"].Value)
                .Matches("[?]")
                .WithMessage(usarIdioma["msgNaoEPergunta"].Value);
        }
    }
}

