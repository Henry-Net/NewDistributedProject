using System;
using System.Collections.Generic;
using System.Text;

namespace EntityModels.Resource
{
    public class CommodityBasicInfo
    {
        public int Id { get; set; }
        public string CommondityName { get; set; }
        public string CommondityType { get; set; }
        public string Vendor { get; set; }
        public string CommondityRule { get; set; }
        public decimal Price { get; set; }
        public virtual List<CommodityInventory> CommodityInventorys { get; set; }
    }
}
