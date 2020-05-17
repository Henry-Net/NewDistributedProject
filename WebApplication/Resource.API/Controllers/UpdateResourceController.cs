using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityModels.Resource;
using Microsoft.AspNetCore.Mvc;
using Resource.API.Dtos;

namespace Resource.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UpdateResourceController : AuthenticationController
    {

        private readonly ResourceDbContext _resourceDbContext;

        public UpdateResourceController(ResourceDbContext resourceDbContext)
        {
            _resourceDbContext = resourceDbContext;
        }

        //#region 添加数据
        //[HttpPost]
        //public async Task<IActionResult> AddCommodity()
        //{
        //    var commondityName = Request.Form["CommondityName"];
        //    var commondityRule = Request.Form["CommondityRule"];
        //    var commondityType = Request.Form["CommondityType"];
        //    var vendor = Request.Form["Vendor"];
        //    if (string.IsNullOrEmpty(Request.Form["Price"]))
        //    {
        //        return BadRequest("Price null");
        //    }
        //    var Price = Convert.ToDecimal(Request.Form["Price"]);

        //    var mod = new CommodityBasicInfo()
        //    {
        //        CommondityName = commondityName,
        //        CommondityRule = commondityRule,
        //        CommondityType = commondityType,
        //        Vendor = vendor,
        //        Price = Price
        //    };

        //    bool result = false;
        //    _resourceDbContext.CommodityBasicInfo.Add(mod);
        //    result = await _resourceDbContext.SaveChangesAsync() > 0;

        //    return Ok(result);
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddCommodityInventory()
        //{
        //    var remark = Request.Form["Remark"];
        //    if (string.IsNullOrEmpty(Request.Form["CommondityId"]) || string.IsNullOrEmpty(Request.Form["InventoryCount"]))
        //    {
        //        return BadRequest(" null");
        //    }

        //    var commondityId = Convert.ToInt32(Request.Form["CommondityId"]);
        //    var inventoryCount = Convert.ToInt32(Request.Form["InventoryCount"]);
        //    var activeEnd = Convert.ToDateTime(Request.Form["ActiveEnd"]);
        //    var activeStart = Convert.ToDateTime(Request.Form["ActiveStart"]);

        //    var mod = new CommodityInventory()
        //    {
        //        CommondityId = commondityId,
        //        ActiveEnd = activeEnd,
        //        ActiveStart = activeStart,
        //        InventoryCount = inventoryCount,
        //        Remark = remark
        //    };

        //    bool result = false;
        //    _resourceDbContext.CommodityInventory.Add(mod);
        //    result = await _resourceDbContext.SaveChangesAsync() > 0;

        //    return Ok(result);
        //}
        //#endregion

        //#region 删除数据

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCommodity(int id)
        //{
        //    bool result = false;
        //    using (var trans = _resourceDbContext.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            var basicInfo = _resourceDbContext.CommodityBasicInfo.FirstOrDefault(c => c.Id == id);
        //            if (basicInfo!=null)
        //            {
        //                var inventoryList = _resourceDbContext.CommodityInventory.Where(i => i.CommondityId == id);
        //                _resourceDbContext.CommodityInventory.RemoveRange(inventoryList);
        //                _resourceDbContext.CommodityBasicInfo.Remove(basicInfo);
        //                result = await _resourceDbContext.SaveChangesAsync() > 0;
        //                trans.Commit();
        //            }
                   
        //        }
        //        catch (Exception)
        //        {
        //            result = false;
        //        }
        //    }
        //    return Ok(result);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCommodityInventory(int id)
        //{
        //    bool result = false;
        //    using (var trans = _resourceDbContext.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            var inventoryList = _resourceDbContext.CommodityInventory.Where(i => i.Id == id);
        //            _resourceDbContext.CommodityInventory.RemoveRange(inventoryList);
        //            result = await _resourceDbContext.SaveChangesAsync() > 0;
        //            trans.Commit();
        //        }
        //        catch (Exception)
        //        {
        //            result = false;
        //        }
        //    }
        //    return Ok(result);
        //}

        //#endregion

        //#region 修改数据

        ////[HttpDelete("{id}")]
        ////public async Task<IActionResult> DeleteCommodity(int id)
        ////{
        ////    bool result = false;
        ////    using (var trans = _resourceDbContext.Database.BeginTransaction())
        ////    {
        ////        try
        ////        {
        ////            var basicInfo = _resourceDbContext.CommodityBasicInfo.FirstOrDefault(c => c.Id == id);
        ////            if (basicInfo != null)
        ////            {
        ////                var inventoryList = _resourceDbContext.CommodityInventory.Where(i => i.CommondityId == id);
        ////                _resourceDbContext.CommodityInventory.RemoveRange(inventoryList);
        ////                _resourceDbContext.CommodityBasicInfo.Remove(basicInfo);
        ////                result = await _resourceDbContext.SaveChangesAsync() > 0;
        ////                trans.Commit();
        ////            }

        ////        }
        ////        catch (Exception)
        ////        {
        ////            result = false;
        ////        }
        ////    }
        ////    return Ok(result);
        ////}

        //#endregion





    }
}
