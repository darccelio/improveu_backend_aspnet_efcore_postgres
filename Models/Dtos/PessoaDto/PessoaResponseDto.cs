namespace ImproveU_backend.Models.Dtos.PessoaDto;

public class PessoaResponseDto
{
    public int Id { get; set; }

    public string Cpf { get; set; }
    public string Nome { get; set; }
    public int UsuarioId { get; set; }
    public string DataCriacao { get; set; }
    public string UltimaAlteracao { get; set; }

    //public PessoaResponseDto(Pessoa pessoa)
    //{
    //    Id = pessoa.Id;
    //    Cpf = pessoa.Cpf;
    //    Nome = pessoa.Nome;
    //    UsuarioId = pessoa.UsuarioId;
    //    DataCriacao = pessoa.DataCriacao.ToShortDateString();
    //    UltimaAlteracao = pessoa.UltimaAlteracao?.ToShortDateString();
    //}
}


