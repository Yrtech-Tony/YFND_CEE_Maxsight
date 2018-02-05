using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using INFI.Web.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace INFI.Web.Controllers
{
    //[Authorize]
    public class TaskController : Controller
    {

        private IHostingEnvironment _environment;
        public TaskController(IHostingEnvironment environment)
        {
            _environment = environment;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult TAS010()
        {
            DateTime now = DateTime.Now;
            ViewBag.StartDate = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd");
            ViewBag.NowDate = now.AddDays(7).ToString("yyyy-MM-dd");
            return View();
        }
        public IActionResult TAS010P()
        {
            return View();
        }
        public IActionResult TAS011P()
        {
            return View();
        }
        public IActionResult TAS012P(string PId)
        {
            ViewBag.PId = PId;
            return View();
        }
        public IActionResult TAS020()
        {
            ViewBag.StartDate = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd");
            ViewBag.NowDate = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }
        public IActionResult TAS020P(string dislist)
        {
            ViewBag.Dislist = dislist;
            ViewBag.StartDate = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd");
            ViewBag.NowDate = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }
        public IActionResult TAS021P(string PId, string PStatus)
        {
            ViewBag.PId = PId;
            ViewBag.PStatus = PStatus;
            ViewBag.StartDate = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd");
            ViewBag.NowDate = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }
        public IActionResult TAS030()
        {
            ViewBag.StartDate = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd");
            ViewBag.NowDate = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }
        public IActionResult TAS030P(string PId, string PStatus)
        {
            ViewBag.PId = PId;
            ViewBag.PStatus = PStatus;
            return View();
        }

        public IActionResult TAS040()
        {
            ViewBag.StartDate = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd");
            ViewBag.NowDate = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }

        public IActionResult TAS050()
        {
            ViewBag.StartDate = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd");
            ViewBag.NowDate = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }

        public IActionResult TAS050P(string TCId)
        {
            ViewBag.TCId = TCId;
            ViewBag.StartDate = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd");
            ViewBag.NowDate = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }
        public IActionResult TAS053P(string TCId)
        {
            ViewBag.TCId = TCId;
            ViewBag.StartDate = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd");
            ViewBag.NowDate = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }

        public IActionResult TAS051P(string TCId)
        {
            ViewBag.TCId = TCId;
            ViewBag.StartDate = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd");
            ViewBag.NowDate = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }
        public IActionResult TAS052P()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> UploadFilesAjax()
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
                    paramList.Add(new KeyValuePair<string, string>("SheetNameList", JsonConvert.SerializeObject(new List<string> { "TaskCard", "TaskItem", "CheckStandard" })));
                    HttpResponseMessage res = await CommonHelper.GetHttpClient1().PostAsync(CommonHelper.Current.GetExcelAPIBaseUrl, new FormUrlEncodedContent(paramList));
                    result = res.Content.ReadAsStringAsync().Result;
                    //Url = Path.Combine(uploads, file.FileName);
                }
                //List<Info> list = new List<Info>();
                //list= ExcelHelpe 
                //string message = $"{files.Count} file(s) / { size}bytes uploaded successfully!";
                return Json(result);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> UploadPlansAjax()
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
                    paramList.Add(new KeyValuePair<string, string>("SheetNameList", JsonConvert.SerializeObject(new List<string> { "Plans", "TaskOfPlans" })));
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
        public class Info
        {
            public string Id { get; set; }
            public string UserName { get; set; }
        }
    }
}
