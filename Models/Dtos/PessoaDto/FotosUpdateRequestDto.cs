using System.ComponentModel.DataAnnotations;

namespace ImproveU_backend.Models.Dtos.PessoaDto;

public record FotosUpdateRequestDto
{

    [Required(ErrorMessage = "O id é obrigatório")]
    public int Id { get; set; }

    [Required(ErrorMessage = "A URL é obrigatória.")]
    [StringLength(200, ErrorMessage = "A URL não pode ter mais de 200 caracteres.")]
    public string path { get; set; }

    public string origem { get; set; }

    [Required(ErrorMessage = "A extensão é obrigatória.")]
    public string extensão { get; set; }

    [Required(ErrorMessage = "O ID da pessoa é obrigatório.")]
    public int PessoaId { get; set; }

}
