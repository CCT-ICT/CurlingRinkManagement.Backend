using System.Text.Json.Serialization;

namespace CurlingRinkManagement.Core.Data.Models.Authentik;
public class TokenResult
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;
}

