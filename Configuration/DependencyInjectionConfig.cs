using ImproveU_backend.Services;
using ImproveU_backend.Services.Interfaces;
using ImproveU_backend.Services.Interfaces.ITreino;

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
        //services.AddScoped<ITreinoService, TreinoService>();


        

        return services;
    }
}