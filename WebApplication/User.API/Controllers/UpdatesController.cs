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
    //[ApiController]
    public class UpdatesController : AuthenticationController
    {
        private readonly UserDbContext _userDbContext;

        public UpdatesController(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }



    }
}
