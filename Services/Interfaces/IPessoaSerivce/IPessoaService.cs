using ImproveU_backend.Models.Dtos.PessoaDto;

namespace ImproveU_backend.Services.Interfaces.IPessoaSerivce;

public interface IPessoaService
{
    public Task<PessoaResponseDto> CriarAsync(PessoaCreateRequestDto pessoaRequest);
    public Task<PessoaResponseDto> BuscarPorCpfAsync(string cpf);

    //public Task<PessoaResponseDto> BuscarPorIdAsync(int id);
    //public Task<IEnumerable<PessoaResponseDto>> BuscarAsync(int skip, int take);
    //public Task<PessoaResponseDto> BuscarPorEmailAsync(string email);

    //public Task AtualizarAsync(int id, UsuarioUpdateRequestDto usuarioRequest);
    //public Task DeletarPorIdAsync(int id);
}
