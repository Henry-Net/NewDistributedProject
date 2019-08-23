using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace IdentityService.Authentication
{
    public class ProfileService : IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));
            var subjectId = subject.Claims.Where(c => c.Type == "sub").FirstOrDefault().Value;

            if (!int.TryParse(subjectId,out int userId))
            {
                throw new ArgumentException(nameof(context.Subject));
            }
            context.IssuedClaims = context.Subject.Claims.ToList();
            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));
            var subjectId = subject.Claims.Where(c => c.Type == "sub").FirstOrDefault().Value;
            context.IsActive = int.TryParse(subjectId, out int userId);

            return Task.CompletedTask;
        }
    }
}
