using Microsoft.AspNetCore.Mvc;

namespace CurlingRinkManagement.Common.Api.Authorization;
public class GroupAuthorizationAttribute : TypeFilterAttribute
{
    public GroupAuthorizationAttribute(string permission) : base(typeof(GroupAuthorizationHandler))
    {
        Arguments = new object[] { permission };
    }
}

