namespace ImproveU_backend.Models.Dtos.PessoaDto;

public record AlunoCreateRequestDto
{
    public PessoaCreateRequestDto PessoaCreateRequest { get; set; }

}