using EntityModels.Common;
using EntityModels.Resource.Merchants;
using Microsoft.AspNetCore.Mvc;
using Resource.API.Serves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.API.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class MerchantsGoodsController : ControllerBase
    {
        private readonly IBaseServe<Merchants_Goods> _baseServe;
        public MerchantsGoodsController(IBaseServe<Merchants_Goods> baseServe)
        {
            _baseServe = baseServe;
        }

        [HttpGet]
        public async Task<ActionResult<ResultList<Merchants_Goods>>> Get()
        {
            
            var list = await _baseServe.GetModelsAsync();
            var l1 = await _baseServe.GetListAsync(l => l.Id == 1);
            return Ok(list);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResultModel<Merchants_Goods>>> Get(int id)
        {
            var model = await _baseServe.GetModelAsync(m=>m.Id == id);
            return Ok(model);
        }
    }
}
