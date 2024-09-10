using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LENA.WebAppCase.Repository.Context;

public class AppIdentityContext : IdentityDbContext<IdentityUser>
{
    public AppIdentityContext(DbContextOptions<AppIdentityContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
