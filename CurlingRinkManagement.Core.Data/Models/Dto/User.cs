namespace CurlingRinkManagement.Core.Data.Models.Dto;
public class User
{
    public Guid Identity { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
}

