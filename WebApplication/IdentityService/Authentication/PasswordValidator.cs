using IdentityServer4.Models;
using IdentityServer4.Validation;
using IdentityService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityService.Authentication
{
    public class PasswordValidator: IExtensionGrantValidator
    {

        private readonly IValidService _validService;
        private readonly IUserService _userService;

        public PasswordValidator(IValidService validService, IUserService userService)
        {
            _validService = validService;
            _userService = userService;
        }

        public string GrantType => "name_password";

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var userName = context.Request.Raw["userName"];
            var password = context.Request.Raw["password"];

            GrantValidationResult erroValidationResult = new GrantValidationResult(TokenRequestErrors.InvalidGrant);

            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                context.Result = erroValidationResult;
                return;
            }
            if (!_validService.VaildUserName(userName))
            {
                context.Result = erroValidationResult;
                return;
            }

            var userIdentityInfo = await _userService.CheckByPassword(userName, password);
            if (userIdentityInfo == null)
            {
                context.Result = erroValidationResult;
                return;
            }
            var claims = new Claim[] {
                new Claim("UserBasicInfoId",userIdentityInfo.UserBasicInfoId.ToString()??string.Empty),
                new Claim("UserName",userIdentityInfo.UserName??string.Empty),
                new Claim("RoleType",userIdentityInfo.RoleType.ToString()??string.Empty)

            };
            context.Result = new GrantValidationResult(userIdentityInfo.UserId.ToString(), GrantType, claims);

        }
    }
}
