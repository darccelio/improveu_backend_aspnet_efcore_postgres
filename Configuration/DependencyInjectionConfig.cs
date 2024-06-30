using ImproveU_backend.Extensions;
using ImproveU_backend.Services.Interfaces.INotificacoesServices;
using ImproveU_backend.Services.Interfaces.IPessoaServices;
using ImproveU_backend.Services.Interfaces.ITreinoServices;
using ImproveU_backend.Services.Interfaces.IUsuarioServices;
using ImproveU_backend.Services.NotificacoesServices;
using ImproveU_backend.Services.PessoaServices;
using ImproveU_backend.Services.PessoasServices;
using ImproveU_backend.Services.TreinoServices;

using ImproveU_backend.Services.UsuariosServices;

namespace ImproveU_backend.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        
        services.AddScoped<IPessoaService, PessoaService>();
        services.AddScoped<IEdFisicoService, EdFisicoService>();
        services.AddScoped<IAlunoService, AlunoService>();
        services.AddScoped<IFotoService, FotoService>();
        services.AddScoped<IExercicioService, ExercicioService>();
        services.AddScoped<ITreinoService, TreinoService>();

        services.AddScoped<INotificadorService, NotificadorServices>();
        services.AddScoped<IUser, UserService>();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUser, AspNetUser>();

        //services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        return services;
    }
}