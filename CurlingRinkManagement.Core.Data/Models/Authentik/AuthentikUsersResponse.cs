using System.Text.Json.Serialization;

namespace CurlingRinkManagement.Core.Data.Models.Authentik;
public class AuthentikUsersResponse
{
    [JsonPropertyName("pagination")]
    public AuthentikPagination Pagination { get; set; } = new();

    [JsonPropertyName("results")]
    public List<AuthentikUser> Results { get; set; } = [];
}

