using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityModels.Resource
{
    public class CommodityInventory
    {
        public int Id { get; set; }
        [ForeignKey("CommodityBasicInfo")]
        public int CommondityId { get; set; }
        public virtual CommodityBasicInfo CommodityBasicInfo { get; set; }
        public int InventoryCount { get; set; }
        public string Remark { get; set; }
        public DateTime ActiveStart { get; set; }
        public DateTime ActiveEnd { get; set; }
    }
}
