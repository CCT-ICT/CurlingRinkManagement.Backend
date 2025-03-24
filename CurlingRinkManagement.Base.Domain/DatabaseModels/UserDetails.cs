using CurlingRinkManagement.Base.Data.Interfaces;

namespace CurlingRinkManagement.Base.Data.DatabaseModels;
public class UserDetails : IDatabaseEntity
{
    public Guid Id { get; set; }
    public DateTime DateTimeCreated { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    //Foreign keys
    public List<Role>? Roles { get; set; }
    public LoginDetails? LoginDetails { get; set; }
}

