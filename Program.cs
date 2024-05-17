using ImproveU_backend.DatabaseConfiguration.Context;
using ImproveU_backend.Services.Interfaces;
using ImproveU_backend.Services;
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

    options.AddPolicy(name: "Production",
        builder => builder.WithOrigins("https://localhost:9000") //url frontend
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

// Add DbContext
builder.Services.AddDbContext<ImproveuContext>(options =>
        options
        .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        provideroptions => { provideroptions.EnableRetryOnFailure(); })
        .EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine, LogLevel.Information));


builder.Services.AddControllers();

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

builder.Services.AddScoped<IUsuarioService, UsuarioService>();

var app = builder.Build();

app.Use(async (context, next) =>
{
    var localAddress = context.Connection.LocalIpAddress.ToString();
    Console.WriteLine($"API ASP.NET Core disponível em: {localAddress}");
    // Continuar com o próximo middleware na pipeline
    await next.Invoke();
});


// Configure the HTTP request pipeline swagger.
if (app.Environment.IsDevelopment()){
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ImproveU API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
    app.UseCors("Development");
}
else
{
    app.UseCors("Production");
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
