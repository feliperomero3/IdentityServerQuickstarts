using Duende.IdentityServer.Models;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
    [
        new IdentityResources.OpenId()
    ];

    public static IEnumerable<ApiScope> ApiScopes =>
    [
        new("api1_access", "API1 default access")
    ];

    public static IEnumerable<Client> Clients =>
    [
        new()
        {
            ClientId = "client",

            // No interactive user, use the clientid/secret for authentication
            AllowedGrantTypes = GrantTypes.ClientCredentials,

            // Secret for authentication
            ClientSecrets =
            {
                new Secret("secret".Sha256())
            },

            // Scopes that client has access to
            AllowedScopes = { "api1_access" }
        }
    ];
}
