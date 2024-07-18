using Microsoft.AspNetCore.Authentication.Cookies;
using PraticaSettimanaleS5.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurazione dell'autenticazione
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt => {
        opt.LoginPath = "/Account/Login";
    });

// Configurazione delle policy di autorizzazione
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("EmployeePolicy", policy => policy.RequireRole("Employee", "Admin")); // Entrambi i ruoli possono accedere
});

// Configurazione del servizio di gestione delle autenticazioni
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IShipmentService, ShipmentService>(); // Aggiungi questa linea per registrare IShipmentService

var app = builder.Build();

// Configure the HTTP request pipeline.
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
