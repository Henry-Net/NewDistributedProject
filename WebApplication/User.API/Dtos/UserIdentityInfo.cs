using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.API.EntityModels;

namespace User.API.Dtos
{
    public class UserIdentityInfo
    {
        /// <summary>
        /// 当前用户ClientUserid 从token subjectId 取得
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 当前用户UserBasicInfoId
        /// </summary>
        public int UserBasicInfoId { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 权限        administrator = 1,ordinary = 2
        /// </summary>
        public AdminRoleType RoleType { get; set; }
    }
}
