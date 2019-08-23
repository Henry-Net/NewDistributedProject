using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User.API.Dtos;
using Microsoft.AspNetCore.Http;
using EntityModels.User;
using Newtonsoft.Json;

namespace User.API.Controllers
{
    [Route("api/[controller]")]
    public class CheckController : ControllerBase
    {
        private readonly UserDbContext _userDbContext;

        public CheckController(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;

        }
        [HttpPost]
        [Route("GetOrCreat")]
        public async Task<IActionResult> GetOrCreateUser(string phone)
        {
            //筛选条件
            if (string.IsNullOrWhiteSpace(phone))
            {
                return BadRequest();
            }
            else
            {
                var userMod = await _userDbContext.ClientUser.AsNoTracking().FirstOrDefaultAsync(user => user.PhoneNumber == phone);
                if (userMod == null)
                {
                    userMod = new ClientUser { PhoneNumber = phone };
                    _userDbContext.Add(userMod);
                    _userDbContext.SaveChanges();
                }
                return Ok(new UserIdentityInfo
                {
                    UserId = userMod.Id,
                    UserName = userMod.ClientUserName,
                    Company = userMod.Company
                });
            }

        }
    }
}