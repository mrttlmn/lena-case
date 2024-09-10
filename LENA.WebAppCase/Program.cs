using Autofac;
using Autofac.Extensions.DependencyInjection;
using LENA.WebAppCase.Modules;
using LENA.WebAppCase.Repository.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<AppIdentityContext>();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(cb => cb.RegisterModule(new DIContainerModule()));

builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionString"]);
});
builder.Services.AddDbContext<AppIdentityContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionString"]);
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var identityContext = services.GetRequiredService<AppIdentityContext>();
    var appContext = services.GetRequiredService<ApplicationDbContext>();

    try
    {
        identityContext.Database.Migrate();
        appContext.Database.Migrate(); 
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Veritabaný migrasyonlarý uygulanýrken bir hata oluþtu: {ex.Message}");
    }
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
