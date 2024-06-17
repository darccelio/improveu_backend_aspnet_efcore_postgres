using ImproveU_backend.Models;
using ImproveU_backend.Services.Interfaces.INotificacoesServices;

namespace ImproveU_backend.Services.NotificacoesServices;

public class NotificadorServices : INotificadorService
{
    private List<Notificacao> _notificacoes;

    public NotificadorServices()
    {
        _notificacoes = new List<Notificacao>();
    }

    public void Handle(Notificacao notificacao)
    {
        _notificacoes.Add(notificacao);
    }

    public List<Notificacao> ObterNotificacoes()
    {
        return _notificacoes;
    }

    public bool TemNotificacao()
    {
        return _notificacoes.Any();
    }
}

