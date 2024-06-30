namespace ImproveU_backend.Models.Dtos.UsuarioDto;

public record UsuarioResponseDto
{
    //public UsuarioResponseDto(Usuario usuario)
    //{
    //    Id = usuario?.Id.ToString();
    //    Email = usuario?.Email;
    //    Papel = usuario?.Papel;
    //    Ativo = usuario?.Ativo;
    //    DataCriacao = usuario?.DataCriacao.ToShortDateString();

    //    if (usuario.UltimaAlteracao is not null)
    //        UltimaAlteracao = usuario.UltimaAlteracao?.ToShortDateString();
    //}

    public string? Id { get; set; }

    public string? Email { get; set; }

    public int? Papel { get; set; }

    public int? Ativo { get; set; }

    public string DataCriacao { get; set; }

    public string UltimaAlteracao { get; set; }

    //public string AccessToken { get; set; }
}
