

using CurlingRinkManagement.Base.Data.DatabaseModels;
using CurlingRinkManagement.Base.Data.Models;

namespace CurlingRinkManagement.Base.Data.Interfaces;

public interface IAuthenticationService
{
    string Register(RegisterModel registerModel);
}

