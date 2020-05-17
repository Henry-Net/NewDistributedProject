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
            ResultList<Merchants_Goods> list = new ResultList<Merchants_Goods>()
            {
                Code = CommonCode.Successful
            };
            list.List = await _baseServe.GetModelsAsync(_=>_.MainImage=="1");
            list.Total = list.List != null ? list.List.Count : 0;
            return Ok(list);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }
    }
}
