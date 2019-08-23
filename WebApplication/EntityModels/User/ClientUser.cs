using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace EntityModels.User
{
    /// <summary>
    /// 登录信息的用户  可以做邮箱或电话号码   登录
    /// </summary>
    public class ClientUser
    {
        public int Id { get; set; }

        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        //用户名登录需要保证用户名字不冲突 
        [MaxLength(20)]
        public string ClientUserName { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(65)]
        public string Password { get; set; }

        [MaxLength(10)]
        public string MassageCode { get; set; }

        [MaxLength(100)]
        public string Company { get; set; }

    }
}
