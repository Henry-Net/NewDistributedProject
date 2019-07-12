using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EntityModels.User;

namespace User.API.Dtos
{
    public class UserDbContext:DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }

        public DbSet<ClientUser> ClientUser { get; set; }

        public DbSet<UserInfo> UserInfo { get; set; }
    }
}
