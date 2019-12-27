using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Services
{
    public interface IValidService
    {
        bool VaildMassageCode(string phone, string massageCode);
        bool VaildUserName(string userName);
    }
}
