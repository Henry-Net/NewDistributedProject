using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace EntityModels.Resource
{
    //https://code.msdn.microsoft.com/Loop-Reference-handling-in-caaffaf7 解决转成json自循环的方案

    public class CommodityInventory
    {
        public int Id { get; set; }
        [ForeignKey("CommodityBasicInfo")]
        public int CommondityId { get; set; }

        [JsonIgnore]  //Newtonsoft.Json 限制循环
        [IgnoreDataMember] //XML 限制循环
        public virtual CommodityBasicInfo CommodityBasicInfo { get; set; }
        public int InventoryCount { get; set; }
        public string Remark { get; set; }
        public DateTime ActiveStart { get; set; }
        public DateTime ActiveEnd { get; set; }
    }
}
