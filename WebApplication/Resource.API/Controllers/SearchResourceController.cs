using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resource.API.Dtos;
using Newtonsoft.Json;

namespace Resource.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class SearchResourceController : ControllerBase
    {
        private readonly ResourceDbContext _resourceDbContext;

        public SearchResourceController(ResourceDbContext resourceDbContext)
        {
            _resourceDbContext = resourceDbContext;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetCommodity()
        {
            var commodity = await _resourceDbContext.CommodityBasicInfo.AsNoTracking().Include(c=>c.CommodityInventorys).ToListAsync();
            return Ok(commodity);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommodityById(int id)
        {
            var commodity = await _resourceDbContext.CommodityBasicInfo.AsNoTracking()
                .FirstOrDefaultAsync(c=>c.Id == id);
            var commodityInventorys = await _resourceDbContext.CommodityInventory
                .AsNoTracking().Where(c => c.CommondityId == id).ToListAsync();
            commodity.CommodityInventorys = commodityInventorys;
            return Ok(commodity);
        }

        
        [HttpGet("{name}")]
        public async Task<IActionResult> GetCommodityByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Ok(null);
            }
            else
            {
                var commodity = await _resourceDbContext.CommodityBasicInfo.AsNoTracking()
                    .FirstOrDefaultAsync(c => c.CommondityName.Contains(name));
                var commodityInventorys = await _resourceDbContext.CommodityInventory
                .AsNoTracking().Where(c => c.CommondityId == commodity.Id).ToListAsync();
                commodity.CommodityInventorys = commodityInventorys;
                return Ok(commodity);
            }
        }


    }
}
