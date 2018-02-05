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
    public class TourController : Controller
    {
        private IHostingEnvironment _environment;
        public TourController(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        //[Authorize]
        //[PermissionRequired]
        public IActionResult TOU010()
        {
            return View();
        }
        //[Authorize]
        //[PermissionRequired]
        public IActionResult TOU020()
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, 1, 1);
            ViewBag.CurrentDate = now.ToString("yyyy-MM-dd");
            ViewBag.FirstDayOfMonth = d1.ToString("yyyy-MM-dd");
            return View();
        }
        //[Authorize]
        //[PermissionRequired]
        public IActionResult TOU010P()
        {
            return View();
        }
        //[Authorize]
        //[PermissionRequired]
        public IActionResult TOU011P()
        {
            return View();
        }
        public IActionResult TOU012P()
        {
            return View();
        }

        public IActionResult TOU013P()
        {
            DateTime now = DateTime.Now.AddDays(7);
            ViewBag.CurrentDate = new DateTime(now.Year, now.Month, now.Day).ToString("yyyy-MM-dd");
            return View();
        }
        //[Authorize]
        //[PermissionRequired]
        public IActionResult TOU020P()
        {
            return View();
        }
        public string Upload()
        {
            try
            {
                List<FileDto> list = new List<FileDto>();
                var uploads = Path.Combine(_environment.WebRootPath, "uploads");
                foreach (var file in Request.Form.Files)
                {
                    using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    //using (FileStream fileStream = System.IO.File.Create(Path.Combine(uploads, file.FileName)))
                    //{
                    //    file.CopyTo(fileStream);
                    //}

                    list.Add(new FileDto() { FileName = file.FileName, Url = "uploads/" + file.FileName });
                }
                return JsonConvert.SerializeObject(list);
            }
            catch (System.Exception e)
            {

                return "";
            }

        }

        [HttpPost]
        public async Task<ActionResult> UploadScoreAjax()
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

                    //string url = CommonHelper.Current.GetExcelAPIBaseUrl + "excel?filepath=" + @"C:\inetpub\INFI.Web\wwwroot\uploads\Template.xlsx&sheetName=Plans";Path.Combine(uploads, file.FileName)
                    // string url = await CommonHelper.GetHttpClient().GetStringAsync(CommonHelper.Current.GetExcelAPIBaseUrl + "excel?filepath=" + @"C:\inetpub\INFI.Web\wwwroot\uploads\Template.xlsx&sheetName=Plans");
                    List<KeyValuePair<string, string>> paramList = new List<KeyValuePair<string, string>>();
                    paramList.Add(new KeyValuePair<string, string>("FullFileName", Path.Combine(uploads, file.FileName)));
                    paramList.Add(new KeyValuePair<string, string>("SheetNameList", JsonConvert.SerializeObject(new List<string> { "Score", "CheckResult" })));
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
        public class FileDto
        {
            public string FileName { get; set; }
            public string Url { get; set; }

        }
    }
}
