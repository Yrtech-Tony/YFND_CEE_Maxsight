using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using INFI.Web.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace INFI.Web.Controllers
{
    //[Authorize]
    public class ImproveController : Controller
    {
        private IHostingEnvironment _environment;
        public ImproveController(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        //[Authorize]
        [PermissionRequired]
        // GET: /<controller>/
        public IActionResult IMP010()
        {
            DateTime now = DateTime.Now;
            ViewBag.CurrentDate = now.ToString("yyyy-MM-dd");
            ViewBag.FirstDay = new DateTime(now.Year, now.Month, 1).ToString("yyyy-MM-dd");
            return View();
        }
        // GET: /<controller>/
        public IActionResult IMP010P()
        {
            return View();
        }
        //[Authorize]
        [PermissionRequired]
        // GET: /<controller>/
        public IActionResult IMP020()
        {
            DateTime now = DateTime.Now;
            ViewBag.CurrentDate = now.ToString("yyyy-MM-dd");
            ViewBag.FirstDay = new DateTime(now.Year, now.Month, 1).ToString("yyyy-MM-dd");
            return View();
        }
        public IActionResult IMP020P()
        {
            ViewBag.CurrentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            return View();
        }
        //[Authorize]
        [PermissionRequired]
        // GET: /<controller>/
        public IActionResult IMP030()
        {
            DateTime now = DateTime.Now;
            ViewBag.CurrentDate = now.ToString("yyyy-MM-dd");
            ViewBag.FirstDay = new DateTime(now.Year, now.Month, 1).ToString("yyyy-MM-dd");
            return View();
        }
        public IActionResult IMP030P()
        {
            ViewBag.CurrentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            return View();
        }

        public async Task<ActionResult> DownLoadForRename(string fileName = "", string sourcepath = "")
        {
            string contentType = "application/octet-stream";
            byte[] bytes = await CommonHelper.Current.HttpGetFileBytes(sourcepath);
            return this.File(bytes, contentType, fileName);
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
                    list.Add(new FileDto() { FileName = file.FileName, Url = "uploads\\" + file.FileName });
                }
                return JsonConvert.SerializeObject(list);
            }
            catch (System.Exception e)
            {

                return "";
            }

        }
        public class FileDto
        {
            public string FileName { get; set; }
            public string Url { get; set; }

        }
        public ActionResult DownLoadAttachment(string fileName = "", string path = "")
        {

            string fullPath = Path.Combine(_environment.WebRootPath, path);
            FileInfo file = new FileInfo(fullPath);
            string contentType = "application/octet-stream";
            Stream stream = null;
            byte[] bytes = null;
            if (file.Exists)
            {
                stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            }
            else
            {

                stream = new FileStream(Path.Combine(_environment.WebRootPath, "uploads", "3.png"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                path = "~/uploads/3.png";
            }

            bytes = new byte[(int)stream.Length];
            stream.Position = 0;
            stream.Read(bytes, 0, bytes.Length);
            stream.Dispose();
            fileName = path.Substring(path.LastIndexOf("/") + 1);
            return this.File(bytes, contentType, fileName);

        }
    }
}
