using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Validation;
using IdentityServer4.Models;
using IdentityService.Services;
using System.Security.Claims;

namespace IdentityService.Authentication
{
    public class SmsAuthCodeValidator : IExtensionGrantValidator
    {
        private readonly IValidService _validService;
        private readonly IUserService _userService;

        public SmsAuthCodeValidator(IValidService validService, IUserService userService)
        {
            _validService = validService;
            _userService = userService;
        }


        public string GrantType => "sms_auth_code";

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            
            var phone = context.Request.Raw["phone"];
            var code = context.Request.Raw["auth_code"];

            GrantValidationResult erroValidationResult = new GrantValidationResult(TokenRequestErrors.InvalidGrant);

            if (string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(code))
            {
                context.Result = erroValidationResult;
                return;
            }
            if (!_validService.VaildMassageCode(phone,code))
            {
                context.Result = erroValidationResult;
                return;
            }

            var userIdentityInfo = await _userService.CheckOrCreate(phone);
            if (userIdentityInfo == null)
            {
                context.Result = erroValidationResult;
                return;
            }
            var claims = new Claim[] {
                new Claim("UserName",userIdentityInfo.UserName??string.Empty),
                new Claim("Company",userIdentityInfo.Company??string.Empty)

            };
            context.Result = new GrantValidationResult(userIdentityInfo.UserId.ToString(), GrantType,claims);

        }
    }
}
