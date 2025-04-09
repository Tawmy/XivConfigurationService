using AspNetCoreExtensions;
using AspNetCoreExtensions.Keycloak;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace XivConfigurationService.Extensions;

internal static class StartupExtensions
{
    public static void AddKeycloakAuthentication(this IServiceCollection services, IConfiguration configuration,
        string authenticationScheme)
    {
        var kcConfig = new KeycloakAuthenticationConfiguration
        {
            OidcAuthority = configuration.GetGuardedConfiguration(EnvironmentVariables.OidcAuthority),
            OidcClientId = configuration.GetGuardedConfiguration(EnvironmentVariables.OidcClientId),
            OidcClientSecret = configuration.GetGuardedConfiguration(EnvironmentVariables.OidcClientSecret)
        };

        services.AddKeycloakAuthentication(kcConfig,
            x => x.AuthenticationScheme = authenticationScheme,
            x =>
            {
                x.Scope.Add("discord");
                x.ClaimActions.MapJsonKey("discord_id", "discord_id");
                x.ClaimActions.MapJsonKey("discord_avatar", "discord_avatar");
            });

        services.ConfigureCookieOidcRefresh(CookieAuthenticationDefaults.AuthenticationScheme,
            authenticationScheme);
    }
}