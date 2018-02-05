using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using INFI.Web.Common;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace INFI.Web.Controllers
{
    //[Authorize]
    public class StatisticController : Controller
    {

        private IHostingEnvironment _environment;
        public StatisticController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        // GET: /<controller>/  
        public IActionResult STA010()
        {
            ViewBag.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
            ViewBag.NowDate = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }

        //[Authorize]
        [PermissionRequired]
        // GET: /<controller>/
        public IActionResult STA020()
        {
            DateTime now = DateTime.Now;
            ViewBag.CurrentDate = now.ToString("yyyy-MM-dd");
            ViewBag.FirstDay = new DateTime(now.Year, now.Month, 1).ToString("yyyy-MM-dd");
            return View();
        }
        public IActionResult STA020P()
        {
            return View();
        }

        //[Authorize]
        [PermissionRequired]
        // GET: /<controller>/
        public IActionResult STA030()
        {
            DateTime now = DateTime.Now;
            ViewBag.CurrentDate = now.ToString("yyyy-MM-dd");
            ViewBag.FirstDay = new DateTime(now.Year, now.Month, 1).ToString("yyyy-MM-dd");
            return View();
        }
        //[Authorize]
        [PermissionRequired]
        // GET: /<controller>/
        public IActionResult STA040()
        {
            DateTime now = DateTime.Now;
            ViewBag.CurrentYear = now.Year;
            ViewBag.CurentMonth = now.Month;
            //ViewBag.CurrentDate = now.ToString("yyyy-MM-dd");
            //ViewBag.FirstDay = new DateTime(now.Year, now.Month, 1).ToString("yyyy-MM-dd");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UploadAftersalesFiguresAjax()
        {
            try
            {
                long size = 0;
                var files = Request.Form.Files;
                var uploads = Path.Combine(_environment.WebRootPath, "uploads");
                string result = "";
                foreach (var file in files)
                {
                    var filename = ContentDispositionHeaderValue
                                    .Parse(file.ContentDisposition)
                                    .FileName
                                    .Trim('"');
                    filename = $@"\{filename}";
                    size += file.Length;
                    using (var fs = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }

                    //string url = CommonHelper.Current.GetExcelAPIBaseUrl + "excel?filepath=" + @"C:\inetpub\INFI.Web\wwwroot\uploads\Template.xlsx&sheetName=Plans";
                    // string url = await CommonHelper.GetHttpClient().GetStringAsync(CommonHelper.Current.GetExcelAPIBaseUrl + "excel?filepath=" + @"C:\inetpub\INFI.Web\wwwroot\uploads\Template.xlsx&sheetName=Plans");
                    List<KeyValuePair<string, string>> paramList = new List<KeyValuePair<string, string>>();
                    paramList.Add(new KeyValuePair<string, string>("FullFileName", Path.Combine(uploads, file.FileName)));
                    paramList.Add(new KeyValuePair<string, string>("SheetNameList", JsonConvert.SerializeObject(new List<string> { "AfterSalesData"})));
                    HttpResponseMessage res = await CommonHelper.GetHttpClient1().PostAsync(CommonHelper.Current.GetExcelAPIBaseUrl, new FormUrlEncodedContent(paramList));
                    result = res.Content.ReadAsStringAsync().Result;
                    //Url = Path.Combine(uploads, file.FileName);
                }
                //List<Info> list = new List<Info>();
                //list= ExcelHelpe 
                //string message = $"{files.Count} file(s) / { size}bytes uploaded successfully!";
                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
