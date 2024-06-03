

using ImproveU_backend.Services;
using ImproveU_backend.Services.Interfaces;

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

        return services;
    }
}