using System.ComponentModel.DataAnnotations;

namespace ImproveU_backend.Models;

public abstract class Base
{
    private DateTime _dataCriacao;

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
    public DateTime? UltimaAlteracao { get; set; }

    public void AtualizaUltimaAlteracao(DateTime date) => UltimaAlteracao = date;
  
}