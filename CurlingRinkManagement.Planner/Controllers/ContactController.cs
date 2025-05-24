using CurlingRinkManagement.Common.Api.Controllers;
using CurlingRinkManagement.Planner.Data.DatabaseModels;
using CurlingRinkManagement.Planner.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurlingRinkManagement.Planner.Controllers;
[ApiController]
[Route("Api/[controller]")]
public class ContactController(IContactService _contactService) : BaseController<Contact>(_contactService)
{

}

