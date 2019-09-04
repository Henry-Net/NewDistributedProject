using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.API.Controllers
{
    [Authorize]
    public class AuthenticationController : ControllerBase
    {

    }
}
