using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FCCopenhagen.Client.Services;
using FCCopenhagen.Client; // Ensure the correct namespace
using System;
using System.Net.Http;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7099/") });
builder.Services.AddScoped<PlayerService>();

await builder.Build().RunAsync();
