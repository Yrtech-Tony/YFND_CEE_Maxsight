using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.Web.Common
{
    public class GlobalAuthorizePolicy : AuthorizationHandler<GlobalAuthorizePolicy>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GlobalAuthorizePolicy requirement)
        {
            context.Succeed(requirement);
            return Task.FromResult(0);
        }

        public override Task HandleAsync(AuthorizationHandlerContext context)
        {
            return base.HandleAsync(context);
        }

        //private bool AuthorizeYN(AuthorizationHandlerContext context)
        //{
        //    string userName = null;
        //    bool succeed = base.AuthorizeCore(httpContext);
        //    if (succeed && httpContext.User.Identity.IsAuthenticated)
        //        userName = httpContext.User.Identity.Name;

        //    //session过期了 但是凭证还在
        //    if (succeed && SessionService.UserInfo.UserId == 0)
        //    {
        //        UserDto userDto = this.UserService.GetUserByUserName(userName);
        //        if (userDto != null)
        //        {
        //            // forms authentication compatability
        //            JavaScriptSerializer serializer = new JavaScriptSerializer();
        //            string userData = serializer.Serialize(userDto);
        //            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
        //                1,
        //                userDto.UserName,
        //                DateTime.Now,
        //                DateTime.Now.AddMonths(1),
        //                false,
        //                userData);
        //            string enc = FormsAuthentication.Encrypt(ticket);
        //            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, enc);
        //            httpContext.Response.Cookies.Add(cookie);
        //            // without reload, apply authentication
        //            SliverantPricipal principal = new SliverantPricipal(userDto.UserName);
        //            HttpContext.Current.User = principal;
        //            SessionService.UserInfo = userDto;
        //        }

        //    }

        //    //如果session不存在了，则权限验证失败
        //    return succeed && SessionService.UserInfo.UserId != 0;
        //}
    }
}
