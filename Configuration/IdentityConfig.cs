using ImproveU_backend.DatabaseConfiguration.Context;
using ImproveU_backend.Extensions;
using ImproveU_backend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ImproveU_backend.Configuration;

public static class IdentityConfig 
{
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
                                                              IConfiguration configuration)
    {
        // Add DbContext
        services.AddDbContext<SecurityContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                provideroptions => { provideroptions.EnableRetryOnFailure(); })
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information));

        // Add Identity
        //services.AddDefaultIdentity<IdentityUser>()
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            // Configurações do Identity
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.User.RequireUniqueEmail = true;
        }).AddRoles<IdentityRole>()
          .AddErrorDescriber<IdentityMensagensPortugues>()
          .AddEntityFrameworkStores<SecurityContext>();

        // Add Identity
        //services.AddDefaultIdentity<IdentityUser>()
        //        .AddRoles<IdentityRole>()
        //        .AddErrorDescriber<IdentityMensagensPortugues>()
        //        .AddEntityFrameworkStores<SecurityContext>();
        //.AddDefaultTokenProviders(); //adiciona recursos para geração de tokens quando for resetar senha

        //JWT Config
        var appSettingsSection = configuration.GetSection("AppSettings");
        services.Configure<AppSettings>(appSettingsSection);
        
        var appSettings = appSettingsSection.Get<AppSettings>();
        var key = Encoding.ASCII.GetBytes(appSettings.Secret);

        services.AddAuthentication(s =>
        {
            s.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            s.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer( j =>
        {
            j.RequireHttpsMetadata = false; //obrigar uso do https
            j.SaveToken = false; //salvar token no httpcontext para ser mais fácil a validação após apresentação do token
            j.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,  //validar o emissor do token
                IssuerSigningKey = new SymmetricSecurityKey(key), // transformação da chave em formato ASCII para chave criptografada
                ValidateIssuer = true, //validacao do emissor pelo nome
                ValidateAudience = true, //validacao da url se é válida conforme origem
                ValidAudience = appSettings.ValidoEm, //informa quais as urls de origem podem ser válidas
                ValidIssuer = appSettings.Emissor //informa quem é o emissor do token (quem é a aplicação que emitiu o token)
            };
        });

        return services;    
    }
}
