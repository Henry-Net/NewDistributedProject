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
    [Route("api/[controller]")]
    public class CheckController : ControllerBase
    {
        private readonly UserDbContext _userDbContext;

        public CheckController(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;

        }

        /// <summary>
        /// 手机号登录或注册
        /// </summary>
        [HttpPost]
        [Route("GetOrCreateUserByPhone")]
        public async Task<IActionResult> GetOrCreateUserByPhone(string phone)
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
                    userMod = new ClientUser { PhoneNumber = phone,ClientUserCode = phone, Password = phone };
                    _userDbContext.Add(userMod);
                    _userDbContext.SaveChanges();
                }
                var userBasicInfo = await _userDbContext.ClientUser_BasicInfo.AsNoTracking().FirstOrDefaultAsync(user => user.ClientUserId == userMod.Id);
                if (userBasicInfo == null)
                {
                    userBasicInfo = new ClientUser_BasicInfo { ClientUserId = userMod.Id, CreateTime = DateTime.Now };
                    _userDbContext.Add(userBasicInfo);
                    _userDbContext.SaveChanges();
                }
                var roletype = AdminRoleType.Vacationer;
                var role_user = await _userDbContext.AdminRole_ClientUserBasicInfo.AsNoTracking().FirstOrDefaultAsync(ru => ru.UserBasicInfoId == userBasicInfo.Id);
                if (role_user!=null)
                {
                    var role = await _userDbContext.AdminRole.AsNoTracking().FirstOrDefaultAsync(r => r.Id == role_user.RoleId);
                    if (role!=null)
                    {
                        roletype = role.RoleType;
                    }
                }
                return Ok(new UserIdentityInfo
                {
                    UserId = userMod.Id,
                    UserBasicInfoId = userBasicInfo.Id,
                    UserName = userBasicInfo.ClientUserName,
                    RoleType = roletype
                });
            }

        }


        /// <summary>
        /// 账号密码登录或注册
        /// </summary>
        [HttpPost]
        [Route("GetUserByCodeAndPassword")]
        public async Task<IActionResult> GetUserByCodeAndPassword(string code,string password)
        {
            //筛选条件
            if (string.IsNullOrWhiteSpace(code)|| string.IsNullOrWhiteSpace(password))
            {
                return BadRequest();
            }
            else
            {
                //可以做多步解密加密
                // 1 . 传输解密
                // 2 . 数据库加密
                //var aesPassword = CommonFunction.CommonFunction.AESEncrypt(password);
                var userMod = await _userDbContext.ClientUser.AsNoTracking().FirstOrDefaultAsync(user => user.ClientUserCode == code && user.Password == password);
                if (userMod == null)
                {
                    return BadRequest();
                }
                var userBasicInfo = await _userDbContext.ClientUser_BasicInfo.AsNoTracking().FirstOrDefaultAsync(user => user.ClientUserId == userMod.Id);
                if (userBasicInfo == null)
                {
                    return BadRequest();
                }
                var roletype = AdminRoleType.Vacationer;
                var role_user = await _userDbContext.AdminRole_ClientUserBasicInfo.AsNoTracking().FirstOrDefaultAsync(ru => ru.UserBasicInfoId == userBasicInfo.Id);
                if (role_user != null)
                {
                    var role = await _userDbContext.AdminRole.AsNoTracking().FirstOrDefaultAsync(r => r.Id == role_user.RoleId);
                    if (role != null)
                    {
                        roletype = role.RoleType;
                    }
                }
                return Ok(new UserIdentityInfo
                {
                    UserId = userMod.Id,
                    UserBasicInfoId = userBasicInfo.Id,
                    UserName = userBasicInfo.ClientUserName,
                    RoleType = roletype
                });
            }

        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var userBasicInfo = await _userDbContext.ClientUser_BasicInfo.AsNoTracking().ToListAsync();
            return Ok(userBasicInfo);
        }
        [HttpGet]
        [Route("GetFirst")]
        public async Task<IActionResult> GetFirst()
        {
            var userBasicInfo = await _userDbContext.ClientUser_BasicInfo.AsNoTracking().FirstOrDefaultAsync();
            return Ok(userBasicInfo);
        }
    }
}