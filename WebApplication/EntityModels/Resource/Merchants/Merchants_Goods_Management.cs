using System;
using System.Collections.Generic;
using System.Text;

namespace EntityModels.Resource.Merchants
{
    public class Merchants_Goods_Management
    {
        public int Id { get; set; }
        public int Goods_Id { get; set; }
        public decimal Price { get; set; }
        public int Inventory { get; set; }
        public string SpecificationsTitle { get; set; }
        public int SpecificationsType { get; set; }
    }
}
