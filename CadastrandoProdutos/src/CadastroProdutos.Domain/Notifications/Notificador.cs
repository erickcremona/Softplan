using CadastroProdutos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroProdutos.Domain.Notifications
{
    public class Notificador : INotificador
    {
        private List<Notificacao> notificacoes;

        public Notificador()
        {
            notificacoes = new List<Notificacao>();
        }

        public List<Notificacao> ObterNotificacoes()
        {
            return notificacoes;
        }

        public bool TemNotificacao()
        {
            return notificacoes.Any();
        }

        public void Handle(Notificacao notificacao)
        {
            notificacoes.Add(notificacao);
        }
    }
}
