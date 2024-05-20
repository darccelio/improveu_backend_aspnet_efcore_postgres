namespace ImproveU_backend.Models.Dtos;

public class AlunoResponseDto
{
   public int Id { get; set; }

    public PessoaResponseDto Pessoa { get; set; }

    public AlunoResponseDto(Aluno aluno)
    {
        Id = aluno.Id;
        Pessoa = new PessoaResponseDto(aluno.Pessoa);
    }
}