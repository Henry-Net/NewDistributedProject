using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User.API.Dtos;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using User.API.EntityModels;

namespace User.API.Controllers
{



    /// <summary>
    /// 查询数据Controller
    /// </summary>
    [Route("api/[controller]")]
    //[ApiController]
    public class ValuesController : AuthenticationController
    {

        private readonly UserDbContext _userDbContext;
        
        public ValuesController(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;

        }

        #region 用户部分

        [HttpGet]
        public async Task<IActionResult> GetAllClientUserBasicInfo()
        {
            if (this.UserIdentity != null && this.UserIdentity.RoleType == AdminRoleType.Administrator)
            {
                var users = await _userDbContext.ClientUser_BasicInfo.AsNoTracking().ToListAsync();
                return Ok(users);
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> GetClientUserBasicInfo()
        {
            var users = await _userDbContext.ClientUser_BasicInfo.AsNoTracking().FirstOrDefaultAsync(c=>c.Id==this.UserIdentity.UserBasicInfoId);
            return Ok(users);
        }

        #endregion


        #region 权限部分

        [HttpGet]
        public async Task<IActionResult> GetAllAdminRole()
        {
            if (this.UserIdentity != null && this.UserIdentity.RoleType == AdminRoleType.Administrator)
            {
                var adminRole = await _userDbContext.AdminRole.AsNoTracking().ToListAsync();
                return Ok(adminRole);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAdminRole()
        {
            var adminRole = await _userDbContext.AdminRole.AsNoTracking().FirstOrDefaultAsync(c => c.RoleType == this.UserIdentity.RoleType);
            return Ok(adminRole);
        }

        #endregion

    }
}
