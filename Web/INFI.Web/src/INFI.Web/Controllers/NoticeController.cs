using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using INFI.Web.Common;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace INFI.Web.Controllers
{
    //[Authorize]
    public class NoticeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {

            return View();
        }
        //[Authorize]
        [PermissionRequired]
        public IActionResult NOT010()
        {
            DateTime now = DateTime.Now;
            ViewBag.CurrentDate = now.ToString("yyyy-MM-dd");
            ViewBag.FirstDay = new DateTime(now.Year, now.Month, 1).ToString("yyyy-MM-dd");
            return View();
        }
        public IActionResult NOT010P()
        {
            ViewBag.CurrentDate = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }
        public IActionResult NOT011P()
        {
            return View();
        }
        public IActionResult NOT012P()
        {
            return View();
        }

        //[Authorize]
        [PermissionRequired]
        public IActionResult NOT020()
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1);
            ViewBag.CurrentDate = now.ToString("yyyy-MM-dd");
            ViewBag.FirstDayOfMonth = d1.ToString("yyyy-MM-dd");
            return View();
        }
        public IActionResult NOT020P()
        {
            return View();
        }
        public IActionResult NOT021P()
        {
            return View();
        }

        //[Authorize]
        [PermissionRequired]
        public IActionResult NOT030()
        {
            ViewBag.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
            ViewBag.NowDate = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }
        public IActionResult NOT030P()
        {
            return View();
        }

        public async Task<ActionResult> DownLoadForRename(string fileName = "", string sourcepath = "")
        {
            string contentType = "application/octet-stream";
            byte[] bytes = await CommonHelper.Current.HttpGetFileBytes(sourcepath);
            return this.File(bytes, contentType, fileName); 
        }
    }
}
