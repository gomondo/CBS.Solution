using CBS.Client;
using CBS.Client.Auth;
using CBS.Client.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// ── Named HttpClient for CBS API ─────────────────────────────────────────────
builder.Services.AddHttpClient("cbs-api", client =>
    client.BaseAddress = new Uri("https://localhost:44398/"))
    .AddHttpMessageHandler<CustomAuthorizationHandler>();

// Default injected HttpClient uses the named client above
builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("cbs-api"));

// ── Handler + Storage ────────────────────────────────────────────────────────
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<CustomAuthorizationHandler>();

// ── Application services ─────────────────────────────────────────────────────
builder.Services.AddScoped<IdentityClientService>();
builder.Services.AddScoped(typeof(IClientService<>), typeof(ClientService<>));

// ── FluentUI components ──────────────────────────────────────────────────────
builder.Services.AddFluentUIComponents();

// ── Auth ─────────────────────────────────────────────────────────────────────
// FIX: Register CustomAuthStateProvider and AuthenticationStateProvider so CascadingAuthenticationState works.

builder.Services.AddScoped<AuthenticationStateProvider>();
builder.Services.AddScoped<CustomAuthStateProvider>(); //add custom provider    
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
