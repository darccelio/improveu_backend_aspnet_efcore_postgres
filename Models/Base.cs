using System.ComponentModel.DataAnnotations;

namespace ImproveU_backend.Models;

public abstract class Base
{
    private DateTime _dataCriacao;
    private DateTime? _ultimaAlteracao;

    public Base()
    {
        DataCriacao = DateTime.Now;
    }

    [Timestamp]
    public DateTime DataCriacao
    {
        get => _dataCriacao;
        private set => _dataCriacao = DateTime.Now;
    }

    [Timestamp]
    public DateTime? UltimaAlteracao
    {
        get => _ultimaAlteracao;
        set => _ultimaAlteracao = DateTime.Now;
    }
}