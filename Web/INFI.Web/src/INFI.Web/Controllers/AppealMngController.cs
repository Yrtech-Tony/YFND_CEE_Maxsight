using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace INFI.Web.Controllers
{
    public class AppealMngController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult APP010()
        {
            ViewBag.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
            ViewBag.NowDate = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }
        public IActionResult APP020()
        {
            ViewBag.StartDate = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd");
            ViewBag.NowDate = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }
        //public IActionResult APP030()
        //{
        //    DateTime now = DateTime.Now;
        //    DateTime d1 = new DateTime(now.Year, now.Month, 1);
        //    ViewBag.CurrentDate = now.ToString("yyyy-MM-dd");
        //    ViewBag.FirstDayOfMonth = d1.ToString("yyyy-MM-dd");
        //    return View();
        //}
        public IActionResult APP040()
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(2018, 5, 1);
            ViewBag.CurrentDate = now.ToString("yyyy-MM-dd");
            ViewBag.FirstDayOfMonth = d1.ToString("yyyy-MM-dd");
            return View();
        }
        public IActionResult APP050()
        {
            DateTime now = DateTime.Now;
            int mouth = 0;
            if (now.Month < 6)
            {
                mouth = 4;
            }
            else
            {
                mouth = 6;
            }
            ViewBag.CurrentDate = now.ToString("yyyy-MM-dd");
            ViewBag.FirstDay = new DateTime(now.Year, 3, 1).ToString("yyyy-MM-dd");
            return View();
        }
        public IActionResult APP040P()
        {
            return View();
        }
        public IActionResult APP041P()
        {
            return View();
        }
    }
}
