using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using RiverEngineeringResources;
using RiverEngineeringResources.ClipboardServiceFolder;
using RiverEngineeringResources.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

builder.Services.AddSingleton<ClipboardService>();
builder.Services.AddSingleton<SharedData>();

await builder.Build().RunAsync();
