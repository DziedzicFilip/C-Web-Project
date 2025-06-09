using Microsoft.AspNetCore.Identity;
using Firma.Data.Data; // Twoje przestrzenie nazw
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Firma.Data.Data.Sklep;
using Firma.Data.Data.CMS;

var builder = WebApplication.CreateBuilder(args);

// DB Context z Identity
builder.Services.AddDbContext<FirmaContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("FirmaContext")
        ?? throw new InvalidOperationException("Connection string 'FirmaContext' not found.")
    ));

// Dodanie usług Identity
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()  // <-- Dodaj wsparcie dla ról
.AddEntityFrameworkStores<FirmaContext>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});
// Ustawienie kultury PL
var cultureInfo = new CultureInfo("pl-PL");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// MVC + Razor
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
var app = builder.Build();

// Tworzenie ról i admina przy starcie
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

    await SeedRolesAndAdminAsync(roleManager, userManager);
}

// Obsługa błędów
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.MapRazorPages();
app.UseAuthentication();
app.UseAuthorization();

// Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapGet("/", context =>
{
    context.Response.Redirect("/Identity/Account/Login");
    return Task.CompletedTask;
});


app.Run();


// Metoda do tworzenia ról i admina
async Task SeedRolesAndAdminAsync(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
{
    // Definiujemy rolę admina
    string adminRole = "Administrator";

    // Sprawdzamy czy rola już istnieje
    if (!await roleManager.RoleExistsAsync(adminRole))
    {
        await roleManager.CreateAsync(new IdentityRole(adminRole));
    }

    // Sprawdzamy czy istnieje użytkownik admin
    string adminEmail = "admin@example.com";
    string adminPassword = "Admin123!"; // Ustaw silne hasło!

    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            // Dodaj inne właściwości jeśli chcesz, np. Imie, Nazwisko itp.
        };

        var result = await userManager.CreateAsync(adminUser, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, adminRole);
        }
        else
        {
            // Obsłuż błędy jeśli chcesz
            throw new Exception("Nie udało się utworzyć użytkownika admina");
        }
    }
    else
    {
        // Jeśli użytkownik istnieje, upewnij się, że ma rolę admina
        if (!await userManager.IsInRoleAsync(adminUser, adminRole))
        {
            await userManager.AddToRoleAsync(adminUser, adminRole);
        }
    }
}
