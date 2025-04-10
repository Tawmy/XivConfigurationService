namespace XivConfigurationService;

internal class EnvironmentVariables
{
    /// <summary>
    ///     OIDC Authority, for Keycloak it's the realm, eg. https://auth.smapone.com/realms/dev
    /// </summary>
    public const string OidcAuthority = "OIDC_AUTHORITY";

    /// <summary>
    ///     OIDC Client ID
    /// </summary>
    public const string OidcClientId = "OIDC_CLIENT_ID";

    /// <summary>
    ///     OIDC Client Secret, do specify this for any and all web apps using a backend. Backend can save secret securely.
    /// </summary>
    /// <remarks>
    ///     While ASP.NET Core correctly uses PKCE by default, Keycloak does not enforce this -> change client config
    /// </remarks>
    public const string OidcClientSecret = "OIDC_CLIENT_SECRET";
}