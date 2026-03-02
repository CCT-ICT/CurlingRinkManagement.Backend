using System.Text.Json.Serialization;

namespace CurlingRinkManagement.Core.Data.Models.Authentik;
[Serializable]
public class AuthentikRole
{
    [JsonPropertyName("pk")]
    public string? Pk { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}

