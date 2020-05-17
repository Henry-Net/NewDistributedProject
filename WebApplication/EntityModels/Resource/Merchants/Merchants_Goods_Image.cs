using System;
using System.Collections.Generic;
using System.Text;

namespace EntityModels.Resource.Merchants
{
    public class Merchants_Goods_Image
    {
        public int Id { get; set; }
        public int Goods_Id { get; set; }
        public string ImageTitle { get; set; }
        public string ImageUrl { get; set; }
        public string ImagePath { get; set; }
    }
}
