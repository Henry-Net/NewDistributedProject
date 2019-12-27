using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace User.API.EntityModels
{

    /// <summary>
    /// 登录信息的用户  可以做邮箱或电话号码   登录
    /// </summary>
    [Table("ClientUser")]
    public class ClientUser
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        //用户名登录需要保证用户名字不冲突 
        [MaxLength(20)]
        public string ClientUserCode { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(65)]
        public string Password { get; set; }


    }

    [Table("ClientUser_BasicInfo")]
    public class ClientUser_BasicInfo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ClientUserId { get; set; }

        //用户名登录需要保证用户名字不冲突 
        [MaxLength(20)]
        public string ClientUserName { get; set; }

        public string Gender { get; set; }

        public DateTime Birthday { get; set; }

        public DateTime CreateTime { get; set; }

        public int State { get; set; }

    }

}
