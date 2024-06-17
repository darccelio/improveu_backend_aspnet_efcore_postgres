namespace ImproveU_backend.Models.Dtos.PessoaDto;

public record PessoaResponseDto
{
    public int Id { get; init; }
    public string Cpf { get; init; }
    public string Nome { get; init; }
    public Guid UsuarioId { get; init; }
    public string DataCriacao { get; init; }
    public string UltimaAlteracao { get; init; }
}


