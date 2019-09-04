using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityModels.Resource;
using Microsoft.EntityFrameworkCore;


namespace Resource.API.Dtos
{
    public class ResourceDbContext:DbContext
    {
        public ResourceDbContext(DbContextOptions<ResourceDbContext> options)
            : base(options)
        {
        }

        public DbSet<CommodityBasicInfo> CommodityBasicInfo { get; set; }
        public DbSet<CommodityInventory> CommodityInventory { get; set; }
        
    }
}
