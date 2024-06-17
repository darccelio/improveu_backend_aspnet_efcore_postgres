using ImproveU_backend.Models;

namespace ImproveU_backend.Services.Interfaces.INotificacoesServices;

public interface INotificadorService
{
    bool TemNotificacao();
    List<Notificacao> ObterNotificacoes();
    void Handle(Notificacao notificacao);
}
