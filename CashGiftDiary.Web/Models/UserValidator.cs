using CashGiftDiary.Web.Repo;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashGiftDiary.Web.Models
{
    public static class UserValidator
    {
        public static async Task ValidateAsync(CookieValidatePrincipalContext context)
        {
            var userRepository = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
            var userPrincipal = context.Principal;
            bool validateResult = false;
            if (userPrincipal!=null)
            {
                var result = userRepository.GetUserByPhone(userPrincipal.Identity.Name);
                validateResult = result!=null;
            }
            if (!validateResult)
            {
                context.RejectPrincipal();
                await context.HttpContext.Authentication.SignOutAsync(Constant.PROJECT_SCHEMA);
            }
        }
    }
}
