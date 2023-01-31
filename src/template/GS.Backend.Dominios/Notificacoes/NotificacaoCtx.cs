using System;
using System.Linq;
using FluentValidation.Results;

namespace GS.Backend.Dominios.Notificacoes
{
    public class NotificacaoCtx
    {
        public NotificacaoCtx()
        {
            _notificacoes = new List<Notificacao>();
        }

        private readonly List<Notificacao> _notificacoes;
        public IReadOnlyCollection<Notificacao> Notificacoes => _notificacoes;
        public bool TemNotificacoes => _notificacoes.Any();

        public void AdicionarNotificacao(string chave, string mensagem)
        {
            _notificacoes.Add(new Notificacao(chave, mensagem));
        }

        public void AdicionarNotificacao(Notificacao notificacao)
        {
            _notificacoes.Add(notificacao);
        }

        public void AdicionarNotificacoes(IReadOnlyCollection<Notificacao> notificacoes)
        {
            _notificacoes.AddRange(notificacoes);
        }

        public void AdicionarNotificacoes(IList<Notificacao> notificacoes)
        {
            _notificacoes.AddRange(notificacoes);
        }

        public void AdicionarNotificacoes(ICollection<Notificacao> notificacoes)
        {
            _notificacoes.AddRange(notificacoes);
        }

        public void AdicionarNotificacoes(ValidationResult validationResult)
        {
            validationResult.Errors.ForEach(item => {
                AdicionarNotificacao(item.ErrorCode, item.ErrorMessage);
            });
        }
    }
}

