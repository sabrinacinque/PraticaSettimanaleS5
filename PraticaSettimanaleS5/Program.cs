using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PraticaSettimanaleS5.Services;

var builder = WebApplication.CreateBuilder(args);

// Aggiungere servizi al contenitore.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
