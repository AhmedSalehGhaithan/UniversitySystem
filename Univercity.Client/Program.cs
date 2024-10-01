using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Univercity.Client;
using Univercity.Client.Service;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<UniService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7161");
});


await builder.Build().RunAsync();
