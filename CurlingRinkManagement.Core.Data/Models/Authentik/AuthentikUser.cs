
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CurlingRinkManagement.Core.Data.Models.Authentik;
[Serializable]
public class AuthentikUser
{
    [JsonPropertyName("pk")]
    public int Pk { get; set; }

    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("is_active")]
    public bool IsActive { get; set; }

    [JsonPropertyName("last_login")]
    public DateTimeOffset? LastLogin { get; set; }

    [JsonPropertyName("date_joined")]
    public DateTimeOffset DateJoined { get; set; }

    [JsonPropertyName("is_superuser")]
    public bool IsSuperuser { get; set; }

    [JsonPropertyName("groups")]
    public List<string> Groups { get; set; } = new();

    [JsonPropertyName("groups_obj")]
    public List<AuthentikGroup> GroupsObj { get; set; } = new();

    [JsonPropertyName("roles")]
    public List<string> Roles { get; set; } = new();
    [JsonPropertyName("roles_obj")]
    public List<AuthentikRole> RolesObj { get; set; } = new();

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("avatar")]
    public string? Avatar { get; set; }

    [JsonPropertyName("attributes")]
    public Dictionary<string, JsonElement> Attributes { get; set; } = new();

    [JsonPropertyName("uid")]
    public string? Uid { get; set; }

    [JsonPropertyName("path")]
    public string? Path { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("uuid")]
    public Guid Uuid { get; set; }

    [JsonPropertyName("password_change_date")]
    public DateTimeOffset PasswordChangeDate { get; set; }

    [JsonPropertyName("last_updated")]
    public DateTimeOffset LastUpdated { get; set; }
}

