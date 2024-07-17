using Microsoft.AspNetCore.Authentication.Cookies;
using PraticaSettimanaleS5.Services;

var builder = WebApplication.CreateBuilder(args);

// Aggiungere servizi al contenitore.
builder.Services.AddControllersWithViews();

// Configurazione dell'autenticazione
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        // Pagina alla quale l'utente sarà indirizzato se non è stato già riconosciuto
        opt.LoginPath = "/Account/Login";
    });

// Configurazione del servizio di gestione delle autenticazioni
builder.Services.AddScoped<IAuthService, AuthService>();

// Registrare il servizio ShipmentService
builder.Services.AddTransient<IShipmentService, ShipmentService>();

var app = builder.Build();

// Configurazione della pipeline delle richieste HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
