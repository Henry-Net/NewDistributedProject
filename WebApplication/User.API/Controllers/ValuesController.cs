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
    public class ValuesController : AuthenticationController
    {

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


        


    }
}
