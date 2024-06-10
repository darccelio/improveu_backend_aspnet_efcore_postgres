using ImproveU_backend.Configuration;
using ImproveU_backend.DatabaseConfiguration.Configuration;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//Cors configuration enable to allow requests from any origin
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Development",
                      builder =>
                      {
                          builder.AllowAnyOrigin()
                                 .AllowAnyMethod()
                                 .AllowAnyHeader();
                      });

    //options.AddPolicy(name: "Production",
    //                 builder => builder.WithOrigins("https://localhost:80") //url frontend
    //                                   .AllowAnyHeader()
    //                                   .AllowAnyMethod());

    options.AddPolicy(name: "Production",
                      builder =>
                      {
                          builder.AllowAnyOrigin()
                                 .AllowAnyMethod()
                                 .AllowAnyHeader();
                      });
});

//configuração para limitar o recebimento de imagens de até 50MB
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 52428800; // 50 MB
});


// Add DbContext
builder.Services.AddDbContext<ImproveuContext>(options =>
        options
        .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        provideroptions => { provideroptions.EnableRetryOnFailure(); })
        .EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine, LogLevel.Information));


builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));

// Add Swagger services
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ImproveU API", Version = "v1" });
});

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5001, listenOptions => listenOptions.UseConnectionLogging());
    serverOptions.ListenAnyIP(5000, listenOptions => listenOptions.UseHttps().UseConnectionLogging());
});

builder.Services.ResolveDependencies();

var app = builder.Build();

app.Use(async (context, next) =>
{
    var localAddress = context.Connection.LocalIpAddress.ToString();
    Console.WriteLine($"API ASP.NET Core disponível em: {localAddress}");

    // Continuar com o próximo middleware na pipeline
    await next.Invoke();
});


// Configure the HTTP request pipeline swagger.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ImproveU API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root path
    });

    app.UseCors("Development");

    app.Logger.LogInformation(@" ###### ############################## #########
                                        Development environment 
                                 ###### ############################## #########");
}
else
{

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ImproveU API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root path
    });
    app.UseCors("Production");

    /* When UseHsts() is called, it adds the Strict-Transport - Security HTTP header to the responses sent by your application.This header tells browsers that your site should only be accessed using HTTPS, not HTTP, and that this policy should be remembered for a certain amount of time.
     Please note that HSTS should be used with caution, as incorrectly configuring it can cause your site to become inaccessible.
     */
    //app.UseHsts();

    app.Logger.LogInformation(@" %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                                        Production environment 
                                 %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%"
    );
}
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


// Configurar fuso horário padrão para UTC em todo o aplicativo
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


app.Run();