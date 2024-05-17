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
builder.Services.AddScoped<IPessoaService, PessoaService>();
builder.Services.AddScoped<IEdFisicoService, EdFisicoService>();

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
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ImproveU API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root path
    });

    app.UseCors("Production");

    app.Logger.LogInformation(@" %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

                                        Production environment 

                                 %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

if(app.Configuration.GetRequiredSection("DatabaseConfiguration:UseMigration").Get<bool>())
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ImproveuContext>();

    if (context.Database.CanConnect())
    {
        try { context.Database.EnsureCreated(); }
        catch (Exception ex) { app.Logger.LogError(ex.Message, "Error on create database"); }

        if (context.Database.HasPendingModelChanges()) {
            context.Database.Migrate();
            app.Logger.LogInformation("Database migrated");
        }
    }
}

app.Run();