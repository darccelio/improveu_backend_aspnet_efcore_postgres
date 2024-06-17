using ImproveU_backend.Models;
using ImproveU_backend.Services.Interfaces.Notificacoes;

namespace ImproveU_backend.Services.NotificacoesServices;

public class NotificadorService : INotificadorService
{
    private List<Notificacao> _notificacoes;

    public NotificadorService()
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

