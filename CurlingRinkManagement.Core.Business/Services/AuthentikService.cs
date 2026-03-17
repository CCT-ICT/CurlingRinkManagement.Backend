using CurlingRinkManagement.Core.Data.Interfaces;
using CurlingRinkManagement.Core.Data.Models.Authentik;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace CurlingRinkManagement.Core.Business.Services;
public class AuthentikService : IUserdataService
{
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _url;
    private readonly string _scope;

    public AuthentikService(IConfiguration configuration)
    {
        var section = configuration.GetSection("AuthentikSettings");
        _clientId = section["ClientId"] ?? throw new Exception("No clientid defined in configuration");
        _clientSecret = section["ClientSecret"] ?? throw new Exception("No clientSecret defined in configuration");
        _url = section["Url"] ?? throw new Exception("No url defined in configuration");
        _scope = section["Scope"] ?? throw new Exception("No url defined in configuration");
    }

    private async Task<string> GetAccessToken()
    {
        var urlContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
            new KeyValuePair<string, string>("client_id", _clientId),
            new KeyValuePair<string, string>("client_secret", _clientSecret),
            new KeyValuePair<string, string>("scope", _scope),
        });

        var httpClient = new HttpClient();
        var response = await httpClient.PostAsync($"{_url}/application/o/token/", urlContent);
        var content = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();
        var token = JsonSerializer.Deserialize<TokenResult>(content)?.AccessToken;
        
        if(token == null) throw new Exception( "Problem deserializing token"); 

        return token;
    }

    public async Task<AuthentikUsersResponse> GetUsers(string group, string? search)
    {
        var httpClient = new HttpClient();

        var request = new HttpRequestMessage(HttpMethod.Get, $"{_url}/api/v3/core/users/?groups_by_name={group}&search={search}");
        var token = await GetAccessToken();
        request.Headers.Add("Accept", "application/json");
        request.Headers.Add("Authorization", $"Bearer {token}");
        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var deserialized = JsonSerializer.Deserialize<AuthentikUsersResponse>(json);
        if (deserialized == null) throw new Exception("Could not serialize authentik users.");

        return deserialized;
    }

}

