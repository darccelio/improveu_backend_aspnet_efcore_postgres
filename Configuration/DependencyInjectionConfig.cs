using ImproveU_backend.Services.Interfaces.IPessoaSerivce;
using ImproveU_backend.Services.Interfaces.ITreino;
using ImproveU_backend.Services.Interfaces.IUsuarioService;
using ImproveU_backend.Services.PessoasServices;
using ImproveU_backend.Services.TreinoService;
using ImproveU_backend.Services.UsuarioService;

namespace ImproveU_backend.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IPessoaService, PessoaService>();
        services.AddScoped<IEdFisicoService, EdFisicoService>();
        services.AddScoped<IAlunoService, AlunoService>();
        services.AddScoped<IFotoService, FotoService>();
        services.AddScoped<IExercicioService, ExercicioService>();
        services.AddScoped<ITreinoService, TreinoService>();

        return services;
    }
}