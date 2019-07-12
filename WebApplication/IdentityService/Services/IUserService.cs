using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Services
{
    public interface IUserService
    {
        Task<int> CheckOrCreate(string phone);
    }
}
