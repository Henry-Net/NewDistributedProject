using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.API.Dtos
{
    public class UserIdentityInfo
    {
        /// <summary>
        /// 当前用户id 从token subjectId 取得
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户公司
        /// </summary>
        public string Company { get; set; }
    }
}
