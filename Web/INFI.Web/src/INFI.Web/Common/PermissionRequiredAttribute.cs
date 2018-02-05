using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using INFI.Web.Common.Module;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using System.Security.Claims;

namespace INFI.Web.Common
{
    //[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class PermissionRequiredAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string controller = context.RouteData.Values["Controller"].ToString();
            string action = context.RouteData.Values["Action"].ToString();
            var hasAccessRight = HasAccessRight(context, controller, action);
            if (!hasAccessRight)
            {
                context.Result = new RedirectResult("~/Error/Http400");
            }
            base.OnActionExecuting(context);
        }

        #region private

        private bool HasAccessRight(ActionExecutingContext context, string controllerName, string actionName)
        {
            List<RoleInfo> roleDtoList = null;

            //roleDtoList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RoleInfo>>(((ClaimsIdentity)context.HttpContext.User.Identity).FindFirst(ClaimTypes.UserData).Value);
            string roleListStr = context.HttpContext.Request.Cookies["infi_ROLELIST1"] + context.HttpContext.Request.Cookies["infi_ROLELIST2"];
            if (!string.IsNullOrWhiteSpace(roleListStr))
            {
                roleDtoList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RoleInfo>>(roleListStr);
            }
            if (roleDtoList != null)
            {
                roleDtoList = roleDtoList.FindAll(r => r.ParentId != 0);
                if (roleDtoList != null && roleDtoList.Count > 0)
                {
                    if (actionName.ToUpper().EndsWith("P") || (controllerName.ToLower() == "home" && actionName.ToLower() == "index"))
                    {
                        return true;
                    }
                    var roleDto = from p in roleDtoList
                                  where p.Action.ToLower() == actionName.ToLower() && p.Controller.ToLower() == controllerName.ToLower()
                                  select p;
                    if (roleDto != null && roleDto.Count() > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion
    }
}
