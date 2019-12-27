using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.API.EntityModels
{
    public class AdminRole
    {
        public int Id { get; set; }

        public string RoleName { get; set; }

        public DateTime CreateTime { get; set; }

        public AdminRoleType RoleType { get; set; }

        public int Order { get; set; }

        public string Description { get; set; }
    }

    public enum AdminRoleType
    {
        Administrator = 1,
        Ordinary = 2,
        Vacationer = 3
    }


    public class AdminRole_ClientUserBasicInfo
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        public int UserBasicInfoId { get; set; }

        public DateTime CreateTime { get; set; }

        public string Description { get; set; }
    }
}
