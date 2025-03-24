using CurlingRinkManagement.Base.Data.Interfaces;

namespace CurlingRinkManagement.Base.Data.DatabaseModels;

public class Role : IDatabaseEntity
{
    public Guid Id { get; set; }
    public string RoleName { get; set; } = string.Empty;

    public List<UserDetails>? Users { get; set; } = [];

}
