using ImproveU_backend.DatabaseConfiguration.Configuration;
using ImproveU_backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ImproveU_backend.Models.Dtos.PessoaDto;
using ImproveU_backend.Services.Interfaces.IPessoaSerivce;
using Microsoft.AspNetCore.Authorization;

namespace ImproveU_backend.Controllers;

[Authorize]
[Route("api/fotos")]
[ApiController]
public class FotosController : ControllerBase
{
    private readonly ImproveuContext _context;
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly IFotoService _fotosService;
    public FotosController(ImproveuContext context, IWebHostEnvironment hostingEnvironment, IFotoService fotosService)
    {
        _context = context;
        _hostingEnvironment = hostingEnvironment;
        _fotosService = fotosService;
    }

    [HttpPost]
    [RequestSizeLimit(52428800)] //50mb
    [ProducesResponseType(typeof(FotosResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Criar([FromForm] FotosCreateRequestDto fotoDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            FotosMetadadosResponseDto foto = await _fotosService.SalvarFotoUpload(fotoDto);
            return CreatedAtAction(nameof(FotosController.BuscarPorId), new { id = foto.Id }, value: foto);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }

    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(FotosResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FotosResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(FotosResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<FotosResponseDto>> BuscarPorId(int id)
    {
        try
        {
            FotosResponseDto fotosResponseDto = await _fotosService.BuscarPorIdAsync(id);
            return Ok(fotosResponseDto);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }


    }

    //[HttpPost]
    //[RequestSizeLimit(52428800)] //limitação do recebimento de arquivos de imagens de até 50MB
    //public async Task<ActionResult<Foto>> PostFoto([FromForm] IFormFile file, int pessoaId)
    //{
    //    if (file == null || file.Length == 0)
    //    {
    //        return BadRequest("Nenhum arquivo foi enviado.");
    //    }

    //    if (!IsImageFile(file))
    //    {
    //        return BadRequest("O arquivo enviado não é uma imagem válida.");
    //    }

    //    string fileName = Path.GetFileName(file.FileName);
    //    string fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
    //    string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", fileName);

    //    using (var stream = new FileStream(filePath, FileMode.Create))
    //    {
    //        await file.CopyToAsync(stream);
    //    }

    //    var foto = new Foto
    //    {
    //        Path = filePath,
    //        Origem = "api",
    //        Extensão = fileExtension,
    //        PessoaId = pessoaId
    //    };

    //    _context.Fotos.Add(foto);
    //    await _context.SaveChangesAsync();

    //    return CreatedAtAction(nameof(GetFotoBase64), new { id = foto.Id }, foto);
    //}

    //private bool IsImageFile(IFormFile file)
    //{
    //    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
    //    return allowedExtensions.Contains(file.ContentType.ToLowerInvariant());
    //}

    //[HttpGet("{id}")]
    //public async Task<ActionResult<Foto>> GetFotoBase64(int id)
    //{
    //    var foto = await _context.Fotos.FindAsync(id);

    //    if (foto == null)
    //    {
    //        return NotFound();
    //    }

    //    using (var ms = new MemoryStream())
    //    {
    //        using (var img = Image.FromFile(foto.Path))
    //        {
    //            img.Save(ms, ImageFormat.Jpeg);
    //        }

    //        var bytes = ms.ToArray();
    //        var base64String = Convert.ToBase64String(bytes);

    //        return base64String;
    //    }
    //}

    //[HttpGet("{id}")]
    //public ActionResult<byte[]> GetFotoArrayByte(int id)
    //{
    //    var foto = _context.Fotos.Find(id);

    //    if (foto == null)
    //    {
    //        return NotFound();
    //    }

    //    using (var ms = new MemoryStream())
    //    {
    //        using (var img = Image.FromFile(foto.Path))
    //        {
    //            img.Save(ms, ImageFormat.Jpeg);
    //        }

    //        return ms.ToArray();
    //    }
    //}


}

