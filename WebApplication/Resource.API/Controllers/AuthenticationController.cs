using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resource.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.API.Controllers
{
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        protected UserIdentityInfo UserIdentity
        {
            get
            {
                var info = new UserIdentityInfo();
                info.UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "sub").Value);
                info.UserName = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
                info.Company = User.Claims.FirstOrDefault(c => c.Type == "Company").Value;
                return info;
            }
        }
    }
}
