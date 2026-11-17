using Dictator.ConsoleInterface;
using Dictator.Web;
using Dictator.Web.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddDictatorGame();
builder.Services.AddScoped<WebSpectrumConsoleService>();
builder.Services.AddScoped<IConsoleService>(sp => sp.GetRequiredService<WebSpectrumConsoleService>());
builder.Services.AddScoped<WebGameRunner>();

await builder.Build().RunAsync();
