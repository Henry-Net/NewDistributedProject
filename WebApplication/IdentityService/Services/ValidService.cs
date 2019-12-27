using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Services
{
    public class ValidService : IValidService
    {
        public bool VaildMassageCode(string phone, string massageCode)
        {
            return true;
        }

        public bool VaildUserName(string userName)
        {
            return true;
        }
    }
}
