namespace ImproveU_backend.Models.Dtos.PessoaDto;

public class EdFisicoResponseDto
{
    public int Id { get; set; }
    public string RegistroConselho { get; set; }
    public PessoaResponseDto PessoaRequest { get; set; }

    //public EdFisicoResponseDto(EdFisico edFisico)
    //{
    //    Id = edFisico.Id;
    //    RegistroConselho = edFisico.RegistroConselho;
    //    Pessoa = new PessoaResponseDto(edFisico.Pessoa);
    //}
}