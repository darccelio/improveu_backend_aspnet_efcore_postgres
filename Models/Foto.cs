namespace ImproveU_backend.Models;

public class Foto : Base
{
    public int Id { get; set; }
    public string Path { get; set; }
    //public string? Origem { get; set; }
    public string Extensão { get; set; }
    public int PessoaId { get; set; }
    public virtual Pessoa Pessoa { get; set; }
}
