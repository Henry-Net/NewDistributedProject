using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resource.API.Dtos;

namespace Resource.API.Controllers
{
    [Route("api/[controller]")]
    public class SearchController:ControllerBase
    {
        private readonly ResourceDbContext _resourceDbContext;

        public SearchController(ResourceDbContext resourceDbContext)
        {
            _resourceDbContext = resourceDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var commodity = await _resourceDbContext.CommodityBasicInfo.AsNoTracking().ToListAsync();
            return Ok(commodity);
        }


    }
}
