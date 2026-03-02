


using CurlingRinkManagement.Core.Data.Models.Dto;

namespace CurlingRinkManagement.Core.Data.Interfaces;
public interface IUserService
{
    Task<UserData> GetUsers(string group);
}

