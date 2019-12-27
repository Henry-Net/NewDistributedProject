using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityModels.User
{
    class ClientUser_BasicInfo
    {
        public int Id { get; set; }

        public string ClientUserId { get; set; }

        public string Gender { get; set; }

        public DateTime Birthday { get; set; }

        public DateTime CreateTime { get; set; }

        public int State { get; set; }
    }
}
