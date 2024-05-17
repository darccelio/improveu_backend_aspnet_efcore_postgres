namespace ImproveU_backend.Models.Dtos;

public class EdFisicoResponseDto
{
    public int Id { get; set; }
    public string RegistroConselho { get; set; }
    public PessoaResponseDto Pessoa { get; set; }

    public EdFisicoResponseDto(EdFisico edFisico)
    {
        Id = edFisico.Id;
        RegistroConselho = edFisico.RegistroConselho;
        Pessoa = new PessoaResponseDto(edFisico.Pessoa);
    }
}