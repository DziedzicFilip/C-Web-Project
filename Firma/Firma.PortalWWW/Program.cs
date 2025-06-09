using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using Firma.Data.Data;
using Firma.Data.Data.Sklep;

var builder = WebApplication.CreateBuilder(args);

// DB Context z Identity
builder.Services.AddDbContext<FirmaContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("FirmaContext")
        ?? throw new InvalidOperationException("Connection string 'FirmaContext' not found.")
    ));

// 💡 Dodanie usług Identity (WAŻNE!)
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // true jeśli chcesz email potwierdzenia
})
.AddEntityFrameworkStores<FirmaContext>();

// Ustawienie kultury PL
var cultureInfo = new CultureInfo("pl-PL");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// MVC + Razor
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Obsługa błędów
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Musi być przed UseAuthorization
app.UseAuthorization();

// Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
