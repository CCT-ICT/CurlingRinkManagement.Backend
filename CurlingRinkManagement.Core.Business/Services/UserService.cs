using CurlingRinkManagement.Core.Data.Interfaces;
using CurlingRinkManagement.Core.Data.Models.Dto;


namespace CurlingRinkManagement.Core.Business.Services;
public class UserService(IUserdataService userdataService) : IUserService
{
    public async Task<Guid> GetMyId(string group, string email, string uid)
    {
        var response = await userdataService.GetUsers(group, email);
        return response.Results.FirstOrDefault(r => r.Uid == uid)?.Uuid ?? throw new KeyNotFoundException("No user");
    }

    public async Task<UserData> GetUsers(string group, string? search)
    {
        var users = await userdataService.GetUsers(group, search);
        var userData = new UserData()
        {
            Next = users.Pagination.Next,
            Previous = users.Pagination.Previous,
            Count = users.Pagination.Count,
            Current = users.Pagination.Current,
            TotalPages = users.Pagination.TotalPages,
            StartIndex = users.Pagination.StartIndex,
            EndIndex = users.Pagination.EndIndex,
            Users = users.Results.Where(u => u.GroupsObj.Any(g => g.Name == group)).Select(u => new User() { Identity = u.Uuid, Avatar = u.Avatar ?? string.Empty, Name = u.Name ?? string.Empty }).ToList()
        };
        return userData;
    }

}
