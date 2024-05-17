using System.ComponentModel.DataAnnotations;

namespace ImproveU_backend.Models.Dtos;

public record UsuarioCreateRequestDto
{
    [Required(ErrorMessage = "O campo email é obrigatório.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo papel é obrigatório.")]
    public int Papel { get; set; }

    [Required(ErrorMessage = "O campo senha é obrigatório.")]
    public string Senha { get; set; }


    public UsuarioCreateRequestDto(int papel, string email, string senha)
    {
        Papel = papel;
        Email = email;
        Senha = senha;
    }

}


