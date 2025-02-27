using System.Text.Json;
using Duende.IdentityModel.Client;

Console.WriteLine("Press any key to continue.");
Console.ReadKey();

const string authority = "https://localhost:5001";

// Discover endpoints from metadata
var client = new HttpClient();
var disco = await client.GetDiscoveryDocumentAsync(authority);

if (disco.IsError)
{
    Console.WriteLine(disco.Error);
    Console.WriteLine(disco.Exception);
    return 1;
}

// Request access token
var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
{
    Address = disco.TokenEndpoint,
    ClientId = "client",
    ClientSecret = "secret",
    Scope = "api1_access"
});

if (tokenResponse.IsError)
{
    Console.WriteLine(tokenResponse.Error);
    Console.WriteLine(tokenResponse.ErrorDescription);
    return 2;
}

Console.WriteLine("Access token: " + tokenResponse.AccessToken);

// Call Protected API
var apiClient = new HttpClient();

apiClient.SetBearerToken(tokenResponse.AccessToken!); // AccessToken is always non-null when IsError is false

var response = await apiClient.GetAsync("https://localhost:6001/identity");

if (!response.IsSuccessStatusCode)
{
    Console.WriteLine(response.StatusCode);
    return 1;
}

var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;

// By default, an access token will contain claims about the scope, lifetime (nbf and exp), the client ID (client_id) and the issuer name (iss).
Console.WriteLine("Response:");
Console.WriteLine(JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true }));

Console.WriteLine("Press any key to exit.");
Console.ReadKey();

return 0;