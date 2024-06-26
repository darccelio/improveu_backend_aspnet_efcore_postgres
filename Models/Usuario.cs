﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImproveU_backend.Models;

public class Usuario : Base
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public Usuario(string email, int papel, string senha)
    {
        Email = email;
        Papel = papel;
        Senha = senha;
        Ativo = 1;
    }

    [Required(ErrorMessage = "O campo email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo email é inválido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo papel é obrigatório.")]
    public int Papel { get; set; }

    [Required(ErrorMessage = "O campo senha é obrigatório.")]
    public string Senha { get; set; }

    public int? Ativo { get; set; }

    public virtual Pessoa Pessoa { get; set; }
}