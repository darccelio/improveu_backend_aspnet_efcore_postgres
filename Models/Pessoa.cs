using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImproveU_backend.Models;

public class Pessoa : Base
{
    public Pessoa(string cpf, string nome, int usuarioId)
    {
        Cpf = cpf;
        Nome = nome;
        UsuarioId = usuarioId;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    [Required(ErrorMessage = "O campo de CPF é obrigatório.")]
    public string Cpf { get; set; }

    [Required(ErrorMessage = "O campo de nome é obrigatório.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "o campo de id do usuário é obrigatório.")]
    // relacionamento com a tabela usuario
    public int UsuarioId { get; set; }

    public virtual Usuario? Usuario { get; set; }

    public virtual EdFisico? EdFisico { get; set; }

    public virtual Aluno? Aluno { get; set; }

}