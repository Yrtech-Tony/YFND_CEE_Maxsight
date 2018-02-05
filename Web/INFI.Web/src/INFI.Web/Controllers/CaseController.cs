using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using INFI.Web.Common;
using System;
using System.Threading.Tasks;

namespace INFI.Web.Controllers
{
    //[Authorize]
    public class CaseController: Controller
    {
        private IHostingEnvironment _environment;
        public CaseController(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        //[Authorize]
        [PermissionRequired]
        // GET: /<controller>/
        public IActionResult CAS010()
        {
            DateTime now = DateTime.Now;
            ViewBag.CurrentDate = now.ToString("yyyy-MM-dd");
            ViewBag.FirstDay = new DateTime(now.Year, now.Month, 1).ToString("yyyy-MM-dd");
            return View();
        }
        public IActionResult CAS010P()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> DownLoadForRename(string fileName = "",string sourcepath = "")
        {
            //var response = await CommonHelper.GetHttpClient().GetAsync(sourcepath);
            //if (response.IsSuccessStatusCode == true)
            //{
            //    var stream = await response.Content.ReadAsStreamAsync();
            //    byte[] bytes = null;
            //    string contentType = "application/octet-stream";
            //    bytes = new byte[(int)stream.Length];
            //    stream.Position = 0;
            //    stream.Read(bytes, 0, bytes.Length);
            //    stream.Dispose();
            //    return this.File(bytes, contentType,fileName);
            //}
            //return null;
            string contentType = "application/octet-stream";
            byte[] bytes = await CommonHelper.Current.HttpGetFileBytes(sourcepath);
            return this.File(bytes, contentType, fileName);
        }
    }
}
