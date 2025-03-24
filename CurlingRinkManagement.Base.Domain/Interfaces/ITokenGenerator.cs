using CurlingRinkManagement.Base.Data.DatabaseModels;

namespace CurlingRinkManagement.Base.Data.Interfaces;
public interface ITokenGenerator
{
    string GenerateToken(string id, string email, List<Role> roles);
}

