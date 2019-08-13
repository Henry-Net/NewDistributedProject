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
    //[ApiController]
    public class ValuesController : ControllerBase
    {

        #region 注释（没用部分）

        //// GET api/values
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        #endregion

        private readonly UserDbContext _userDbContext;
        
        public ValuesController(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userDbContext.ClientUser.AsNoTracking().ToListAsync();
            return Ok(users);
        }


        [HttpPost]
        [Route("GetOrCreat")]
        public async Task<IActionResult> GetOrCreateUser(string phone)
        {
            
            //筛选条件
            if (string.IsNullOrWhiteSpace(phone)||phone=="18516752003")
            {
                return BadRequest();
            }
            else
            {
                var userMod = await _userDbContext.ClientUser.AsNoTracking().FirstOrDefaultAsync(user => user.PhoneNumber == phone);
                if (userMod==null)
                {
                    userMod = new ClientUser { PhoneNumber = phone };
                    _userDbContext.Add(userMod);
                    _userDbContext.SaveChanges();
                }
                return Ok(userMod.Id);
            }
           
        }


    }
}
