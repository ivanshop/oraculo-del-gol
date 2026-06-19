using Oraculo.Web;
using Oraculo.Web.Helpers;
using Oraculo.Web.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7064") });
builder.Services.AddScoped<OraculoService>();
builder.Services.AddScoped<TelegramHelper>();

await builder.Build().RunAsync();
