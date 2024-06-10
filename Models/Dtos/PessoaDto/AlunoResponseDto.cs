namespace ImproveU_backend.Models.Dtos.PessoaDto;

public class AlunoResponseDto
{
    public int Id { get; set; }

    public PessoaResponseDto PessoaRequest { get; set; }

    //public AlunoResponseDto(Aluno aluno)
    //{
    //    Id = aluno.Id;
    //    PessoaRequest = new PessoaResponseDto(aluno.Pessoa);
    //}
}