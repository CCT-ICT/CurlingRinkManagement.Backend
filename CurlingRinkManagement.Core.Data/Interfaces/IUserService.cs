


using CurlingRinkManagement.Core.Data.Models.Dto;

namespace CurlingRinkManagement.Core.Data.Interfaces;
public interface IUserService
{
    Task<Guid> GetMyId(string group, string email, string uid);
    Task<UserData> GetUsers(string group, string? search);
}

