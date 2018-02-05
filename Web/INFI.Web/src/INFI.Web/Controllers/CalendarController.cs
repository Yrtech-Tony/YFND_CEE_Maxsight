using System;
using Microsoft.AspNetCore.Mvc;
using INFI.Web.Common;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace INFI.Web.Controllers
{
    //[Authorize]
    [PermissionRequired]
    public class CalendarController : Controller
    {
        //[Authorize]
        [PermissionRequired]
        public IActionResult CAL010()
        {
            ViewBag.Now = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }
        public IActionResult CAL010P()
        {
            return View();
        }
    }
}
