namespace ImproveU_backend.Models.Dtos;

public record PessoaCreateResquestDto
{
    public string Cpf { get; set; }
    public string Nome { get; set; }
    public int UsuarioId { get; set; }

}

