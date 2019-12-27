using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.API.Dtos;
using User.API.EntityModels;

namespace User.API.Controllers
{
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        protected UserIdentityInfo UserIdentity
        {
            get {
                var info = new UserIdentityInfo();
                info.UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "sub").Value);
                info.UserBasicInfoId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "UserBasicInfoId").Value);
                info.UserName = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
                info.RoleType = (AdminRoleType)Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "sub").Value); 
                return info;
            }
        }
    }
}