using Softplan.CalculoPrecoVenda.Domain.Notificacoes;
using System.Collections.Generic;

namespace Softplan.CalculoPrecoVenda.Domain.Contratos
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
