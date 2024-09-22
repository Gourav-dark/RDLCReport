using RDLC_Report.Client.Pages;
using RDLC_Report.Client.Services;
using RDLC_Report.Components;
using RDLC_Report.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Add service for API controllers
builder.Services.AddControllers();
//Service
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IReservation, Reservation>();
builder.Services.AddScoped(http => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7028")
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// Adding Authentication and Authorization
//app.UseAuthentication();
//app.UseAuthorization();

// For mapping controller
app.MapControllers();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(RDLC_Report.Client._Imports).Assembly);

app.Run();
