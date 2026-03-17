using System.Text.Json;
using System.Text.Json.Serialization;

namespace CurlingRinkManagement.Core.Data.Models.Authentik;
[Serializable]
public class AuthentikGroup
{
    [JsonPropertyName("pk")]
    public string? Pk { get; set; }

    [JsonPropertyName("num_pk")]
    public int NumPk { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("is_superuser")]
    public bool IsSuperuser { get; set; }

    [JsonPropertyName("attributes")]
    public Dictionary<string, JsonElement> Attributes { get; set; } = new();
}

