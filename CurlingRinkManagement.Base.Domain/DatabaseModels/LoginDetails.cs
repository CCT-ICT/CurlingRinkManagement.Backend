using CurlingRinkManagement.Base.Data.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurlingRinkManagement.Base.Data.DatabaseModels;

public class LoginDetails : IDatabaseEntity
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public byte[] Salt { get; set; } = [];

    //ForeignKeys
    [ForeignKey("UserModel")]
    public Guid UserModelId { get; set; }
    public UserDetails? UserModel { get; set; } = new UserDetails();
}
