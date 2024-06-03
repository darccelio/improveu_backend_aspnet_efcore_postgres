using ImproveU_backend.DatabaseConfiguration.Configuration;
using ImproveU_backend.Models;
using ImproveU_backend.Models.Dtos;
using ImproveU_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImproveU_backend.Services;

public class FotoService : IFotoService
{
    private readonly ImproveuContext _context;
    private readonly IPessoaService _pessoaService;

    public FotoService(IPessoaService pessoaService, ImproveuContext context)
    {
        _pessoaService = pessoaService;
        _context = context;
    }

    public Task AtualizarAsync(int id, FotosUpdateRequestDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FotosResponseDto>> BuscarAsync(int skip, int take)
    {
        throw new NotImplementedException();
    }

    public async Task<FotosResponseDto> BuscarPorIdAsync(int id)
    {

        if (id < 1) 
            throw new ArgumentException("Id é inválido.", nameof(id));

        var foto = await _context.Fotos.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);

        if (foto is null)
            throw new ArgumentException("Foto não localizada.");

        if (string.IsNullOrEmpty(foto.Path) || !File.Exists(foto.Path))
        {
            throw new ArgumentException(nameof(foto.Path), "Path informado não localizado.");
        }

        var filePath = foto.Path;
        if (!File.Exists(filePath))
        {
            throw new ArgumentException(nameof(filePath), "Path informado não localizado.");
        }

        var fileType = "static/image/" + foto.Extensão.TrimStart('.'); // Supondo que a extensão esteja no formato ".jpg", ".png", etc.
        var fileBytes = await File.ReadAllBytesAsync(filePath);

        if(fileBytes.Length == 0)
        {
            throw new ArgumentException(nameof(fileBytes), "Arquivo não encontrado.");
        }


        var responseDto = new FotosResponseDto
        {
            Id = foto.Id,
            Path = foto.Path,
            Extensao = foto.Extensão,
            PessoaId = foto.PessoaId,

        };
        //FormFile formFile = new FormFile(new MemoryStream(fileBytes), 0, fileBytes.Length, Path.GetFileName(filePath), Path.GetExtension(filePath))
        //{
        //    Headers = new HeaderDictionary(),
        //    ContentType = "image/jpeg" // ou outro tipo de conteúdo apropriado
        //};
        //responseDto.Foto = formFile;

        var fileBase64 = Convert.ToBase64String(fileBytes);
        responseDto.FotoBase64 = fileBase64;

        return responseDto;
    }

    public Task<FotosMetadadosResponseDto> CriarAsync(FotosCreateRequestDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<FotosMetadadosResponseDto> SalvarFotoUpload(FotosCreateRequestDto fotoDto)
    {
        if (fotoDto is null)
        {
            throw new ArgumentNullException(nameof(fotoDto), "não pode ser null");
        }

        var pessoa = await _context.Pessoas.FindAsync(fotoDto.PessoaId);
        if (pessoa == null)
        {
            throw new ArgumentNullException(nameof(pessoa), "não pode ser null");
        }

        //var filePath = Path.Combine("wwwroot/images", fotoDto.Foto.FileName);
        var filePath = Path.Combine("static/images", fotoDto.Foto.FileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await fotoDto.Foto.CopyToAsync(stream);
        }

        var foto = new Foto
        {
            Path = filePath,
            Extensão = Path.GetExtension(fotoDto.Foto.FileName),
            PessoaId = fotoDto.PessoaId,
            Pessoa = pessoa
        };

        _context.Fotos.Add(foto);
        await _context.SaveChangesAsync();

        var responseDto = new FotosMetadadosResponseDto
        {
            Id = foto.Id,
            Path = foto.Path,
            PessoaId = foto.PessoaId,
           
        };

        return responseDto;
    }
}
