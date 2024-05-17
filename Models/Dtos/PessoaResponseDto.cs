using ImproveU_backend.Migrations;

namespace ImproveU_backend.Models.Dtos;

public class PessoaResponseDto
{

    public int Id { get; set; }

    public string Cpf { get; set; }
    public string Nome { get; set; }

    public int UsuarioId { get; set; }

    public PessoaResponseDto(string cpf, string nome, int usuarioId)
    {
        Cpf = cpf;
        Nome = nome;
        UsuarioId = usuarioId;
    }

    public PessoaResponseDto(Pessoa pessoa)
    {
        Cpf = pessoa.Cpf;
        Nome = pessoa.Nome;
        UsuarioId = pessoa.UsuarioId;
    }
}


