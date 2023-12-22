using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProyectoIdentity.Datos;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseNpgsql(config.GetConnectionString("IdentityDB")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

//Url de retorno al acceder
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/Cuentas/Acceso");
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
