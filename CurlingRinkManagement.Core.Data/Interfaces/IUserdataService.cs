
using CurlingRinkManagement.Core.Data.Models.Authentik;

namespace CurlingRinkManagement.Core.Data.Interfaces;
public interface IUserdataService
{
    Task<AuthentikUsersResponse> GetUsers(string group, string? search);
}

