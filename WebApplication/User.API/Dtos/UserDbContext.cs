using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using User.API.EntityModels;

namespace User.API.Dtos
{
    public class UserDbContext:DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }

        public DbSet<ClientUser> ClientUser { get; set; }

        public DbSet<ClientUser_BasicInfo> ClientUser_BasicInfo { get; set; }

        public DbSet<AdminRole> AdminRole { get; set; }

        public DbSet<AdminRole_ClientUserBasicInfo> AdminRole_ClientUserBasicInfo { get; set; }
    }
}
