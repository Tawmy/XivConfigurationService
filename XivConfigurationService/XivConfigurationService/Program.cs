using AspNetCoreExtensions;
using XivConfigurationService.Components;
using XivConfigurationService.Extensions;
using _Imports = XivConfigurationService.Client._Imports;

var builder = WebApplication.CreateBuilder(args);

const string oidcScheme = "Keycloak";
builder.Services.AddKeycloakAuthentication(builder.Configuration, oidcScheme);
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization(x => x.SerializeAllClaims = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", true);
    app.UseHsts();
}

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(_Imports).Assembly);

app.MapGroup("/authentication").MapLoginAndLogout(oidcScheme);

app.Run();