using ImproveU_backend.DatabaseConfiguration.Configuration;
using ImproveU_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ImproveU_backend.DatabaseConfiguration.Context;
public class SecurityContext : IdentityDbContext
{
    public DbSet<Pessoa> Pessoa { get; set; }
    //public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public SecurityContext(DbContextOptions<SecurityContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurar o Identity para usar a tabela "ApplicationUsers"
        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable(name: "ApplicationUser");
        });

        modelBuilder.Entity<IdentityRole>(entity =>
        {
            entity.ToTable(name: "ApplicationRoles");
        });

        modelBuilder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.ToTable("ApplicationUserRoles");
        });

        modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
        {
            entity.ToTable("ApplicationUserClaims");
        });

        modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.ToTable("ApplicationUserLogins");
        });

        modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
        {
            entity.ToTable("ApplicationRoleClaims");
        });

        modelBuilder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.ToTable("ApplicationUserTokens");
        });

        //// Aplicar as configurações específicas do seu modelo Pessoa
        modelBuilder.ApplyConfiguration(new PessoaDbConfiguration());
    }

}
