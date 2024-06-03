using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ImproveU_backend.Models.Dtos;

public record FotosCreateRequestDto
{


    [Required(ErrorMessage = "A extensão é obrigatória.")]
    public string Extensao { get; set; }

    [Required(ErrorMessage = "O arquivo da imagem é obrigatória.")]
    public IFormFile Foto { get; set; }

    [Required(ErrorMessage = "O ID da pessoa é obrigatório.")]
    public int PessoaId { get; set; }
}
