using System;
using FluentValidation;
using FluentValidation.Results;

namespace GS.Backend.Dominios.Validacoes
{
    public abstract class BaseValidacao
    {
        public bool Valido { get; private set; }
        public bool Invalido => !Valido;
        public ValidationResult ValidacaoResultado { get; private set; }

        public bool Validar<TModel>(TModel modelo, AbstractValidator<TModel> validador)
        {
            ValidacaoResultado = validador.Validate(modelo);
            return Valido = ValidacaoResultado.IsValid;
        }
    }
}

