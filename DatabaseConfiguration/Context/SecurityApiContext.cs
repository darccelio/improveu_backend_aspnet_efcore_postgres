using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ImproveU_backend.DatabaseConfiguration.Context;

public class SecurityApiContext : IdentityDbContext
{
    public SecurityApiContext(DbContextOptions<SecurityApiContext> options) : base(options) { }
}
