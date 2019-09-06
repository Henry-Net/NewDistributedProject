using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Resource.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UpdateResourceController : AuthenticationController
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            
            return new string[] { "value1", "value2", UserIdentity.UserId.ToString() };
        }
    }
}
