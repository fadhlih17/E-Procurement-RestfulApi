using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Procurement.Controllers;

[ApiController]
[Authorize]
public class BaseController : ControllerBase
{
    
}