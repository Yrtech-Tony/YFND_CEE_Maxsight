using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using INFI.Web.Common;
using INFI.Web.Common.Module;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace INFI.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: /<controller>/
        public IActionResult Login()
        {
            //if (!string.IsNullOrWhiteSpace(HttpContext.Session.GetString(SessionKeys.SESSION_USERID)))
            //if (!string.IsNullOrWhiteSpace(HttpContext.Request.Cookies[SessionKeys.SESSION_USERID]))
            //{
            //    return Redirect("/Home/Index");
            //}

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string inputUserID, string inputPassword)
        {
            try
            {
                HttpContext.Session.Clear();
                InitViewState();
                if (string.IsNullOrWhiteSpace(inputUserID))
                {
                    ViewBag.ErrorLoginMessage = "请输入用户名。";
                }
                else if (string.IsNullOrWhiteSpace(inputPassword))
                {
                    ViewBag.ErrorLoginMessage = "请输入密码。";
                }
                else
                {
                    UserInfo userinfo = await SetUserInfo(inputUserID, inputPassword);
                    if (userinfo != null && userinfo.UserId != "0")
                    {
                        ClaimsIdentity ci = new ClaimsIdentity("INFICookieMiddlewareInstance");
                        ci.AddClaim(new Claim(ClaimTypes.Name, userinfo.UserId));
                        //ci.AddClaim(new Claim(ClaimTypes.UserData, Newtonsoft.Json.JsonConvert.SerializeObject(userinfo.RoleList)));

                        //HttpContext.Response.Cookies.Append(SessionKeys.SESSION_USERNAME, userinfo.UserName);
                        //HttpContext.Response.Cookies.Append(SessionKeys.SESSION_PHONE, userinfo.Phone);
                        //HttpContext.Response.Cookies.Append(SessionKeys.SESSION_USERTYPENAME, userinfo.UserTypeName);
                        //HttpContext.Response.Cookies.Append(SessionKeys.SESSION_DISNAME, userinfo.DisName);
                        //HttpContext.Response.Cookies.Append(SessionKeys.SESSION_EMAIL, userinfo.Email);
                        //HttpContext.Response.Cookies.Append(SessionKeys.SESSION_LOGGEDINAT, DateTime.Now.ToString("yyyy-MM-dd"));
                        string roleListStr = Newtonsoft.Json.JsonConvert.SerializeObject(userinfo.RoleList);
                        if (roleListStr.Length > 1501)
                        {
                            HttpContext.Response.Cookies.Append("infi_ROLELIST1", roleListStr.Substring(0, 1500));
                            HttpContext.Response.Cookies.Append("infi_ROLELIST2", roleListStr.Substring(1500, roleListStr.Length - 1500));
                        }
                        else
                        {
                            HttpContext.Response.Cookies.Append("infi_ROLELIST1", roleListStr);
                            HttpContext.Response.Cookies.Append("infi_ROLELIST2", "");
                        }
                        HttpContext.Response.Cookies.Append(SessionKeys.SESSION_USERID, userinfo.UserId);
                        HttpContext.Response.Cookies.Append(SessionKeys.SESSION_NAME, userinfo.Name);
                        HttpContext.Response.Cookies.Append(SessionKeys.SESSION_USERTYPE, userinfo.UserType);
                        HttpContext.Response.Cookies.Append(SessionKeys.SESSION_ORGZIONID, userinfo.OrgZionId);
                        HttpContext.Response.Cookies.Append(SessionKeys.SESSION_ORGAREAID, userinfo.OrgAreaId);
                        HttpContext.Response.Cookies.Append(SessionKeys.SESSION_ORGSERVERID, userinfo.OrgServerId);
                        HttpContext.Response.Cookies.Append(SessionKeys.SESSION_ORGDEPARTMENTID, userinfo.OrgDepartmentId);
                        HttpContext.Response.Cookies.Append(SessionKeys.SESSION_ZIONLIST, Newtonsoft.Json.JsonConvert.SerializeObject(userinfo.ZionList));
                        HttpContext.Response.Cookies.Append(SessionKeys.SESSION_DEPARTMENTLIST, Newtonsoft.Json.JsonConvert.SerializeObject(userinfo.DepartmentList));
                        HttpContext.Response.Cookies.Append("infi_ACCESS_TOKEN", CommonHelper.GenerateToken());
                        HttpContext.Response.Cookies.Append("infi_ABOUT_INDEX", "F");

                        await HttpContext.Authentication.SignInAsync("INFICookieMiddlewareInstance", new ClaimsPrincipal(ci));
                        return Redirect("~/Home/Index");
                    }
                    else
                    {
                        ViewBag.ErrorLoginMessage = "用户名或者密码不正确。";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorLoginMessage = "未查询到用户信息,请重试。";
                Login();
            }
            return View();
        }

        public ActionResult Logout()
        {
            InitViewState();
            return Redirect("/Account/Login");
        }

        public IActionResult Logins()
        {
            if (!string.IsNullOrWhiteSpace(HttpContext.Session.GetString(SessionKeys.SESSION_USERID)))
            {
                return Redirect("/Home/Index");
            }

            return View();
        }

        #region private
        private async Task<UserInfo> SetUserInfo(string inputUserID, string inputPassword)
        {
            string result = await CommonHelper.GetHttpClient().GetStringAsync(CommonHelper.Current.GetAPIBaseUrl + "/Users/GetForBs/" + inputUserID + "/" + inputPassword);

            var apiResult = CommonHelper.DecodeString<APIResult>(result);
            UserInfo userInfo;

            if (apiResult.ResultCode == ResultType.Success)
            {
                userInfo = CommonHelper.DecodeString<UserInfo>(apiResult.Body);
            }
            else
            {
                userInfo = null;
            }
            return userInfo;
        }

        private async void InitViewState()
        {
            HttpContext.Response.Cookies.Delete(SessionKeys.SESSION_USERID);
            HttpContext.Response.Cookies.Delete(SessionKeys.SESSION_USERID);
            HttpContext.Response.Cookies.Delete(SessionKeys.SESSION_NAME);
            HttpContext.Response.Cookies.Delete(SessionKeys.SESSION_USERTYPE);
            HttpContext.Response.Cookies.Delete(SessionKeys.SESSION_ORGZIONID);
            HttpContext.Response.Cookies.Delete(SessionKeys.SESSION_ORGAREAID);
            HttpContext.Response.Cookies.Delete(SessionKeys.SESSION_ORGSERVERID);
            HttpContext.Response.Cookies.Delete(SessionKeys.SESSION_ORGDEPARTMENTID);
            HttpContext.Response.Cookies.Delete(SessionKeys.SESSION_ZIONLIST);
            HttpContext.Response.Cookies.Delete(SessionKeys.SESSION_DEPARTMENTLIST);
            HttpContext.Response.Cookies.Delete("infi_ACCESS_TOKEN");
            HttpContext.Response.Cookies.Delete("infi_ROLELIST1");
            HttpContext.Response.Cookies.Delete("infi_ROLELIST2");
            HttpContext.Session.Clear();
            await HttpContext.Authentication.SignOutAsync("INFICookieMiddlewareInstance");
        }


        #endregion

    }
}
