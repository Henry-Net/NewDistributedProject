using IdentityService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Services
{
    public interface IUserService
    {
        Task<UserIdentityInfo> CheckOrCreateByPhone(string phone);
        Task<UserIdentityInfo> CheckByPassword(string name, string password);
    }
}
