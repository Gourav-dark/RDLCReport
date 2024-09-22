using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RDLC_Report.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
//Base Uri Access Code
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});
builder.Services.AddScoped<IReservation, Reservation>();

await builder.Build().RunAsync();
