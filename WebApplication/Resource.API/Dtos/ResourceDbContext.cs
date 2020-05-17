using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityModels.Resource;
using EntityModels.Resource.Merchants;
using Microsoft.EntityFrameworkCore;


namespace Resource.API.Dtos
{
    public class ResourceDbContext : DbContext
    {
        public ResourceDbContext(DbContextOptions<ResourceDbContext> options)
            : base(options)
        {
        }

        //public DbSet<CommodityBasicInfo> CommodityBasicInfo { get; set; }
        //public DbSet<CommodityInventory> CommodityInventory { get; set; }

        public virtual DbSet<Merchants_Goods> Merchants_Goods { get; set; }
        public virtual DbSet<Merchants_Goods_Image> Merchants_Goods_Image { get; set; }
        public virtual DbSet<Merchants_Goods_Management> Merchants_Goods_Management { get; set; }
    }
}
