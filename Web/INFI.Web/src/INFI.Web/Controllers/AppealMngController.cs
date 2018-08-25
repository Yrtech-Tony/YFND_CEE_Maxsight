using INFI.Web.Common;
using INFI.Web.Common.Module;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace INFI.Web.Controllers
{
    public class AppealMngController : Controller
    {
        private IHostingEnvironment _environment;
        public AppealMngController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

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
            DateTime d1 = new DateTime(2018, 8, 1);
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

        public IActionResult DownloadAttachBatch(string sdate, string edate, string sourceType, string carId, string areaId,
            string zoneId, string disId, string appealResult)
        {
            Task<IEnumerable<AppealListDto>> result = SearchApplealInfoList(sdate, edate, sourceType, carId, areaId, zoneId, disId, appealResult);

            string downloads = Path.Combine(_environment.WebRootPath, "downloads");
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string rootPath = Path.Combine(downloads, timestamp);

            bool hasAttach = false;
            foreach (AppealListDto appealListDto in result.Result)
            {
                string RId = appealListDto.TPId;
                string firstDic = appealListDto.DisCode.Trim() + "_"+appealListDto.DisName.Trim();
                string secondDic = appealListDto.ScoreStandard.Trim();
                var regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
                var reg = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
                secondDic = reg.Replace(secondDic, "");
                string localPath = Path.Combine(rootPath, firstDic, secondDic);

                AppealInfo recInfoDto = GetAppealInfoSearch(RId).Result;
                if (recInfoDto == null || recInfoDto.AttachmentList == null || recInfoDto.AttachmentList.Count == 0)
                {
                    continue;
                }

                if (!Directory.Exists(localPath))
                {
                    Directory.CreateDirectory(localPath);
                }
                foreach (AttachmentMngDto attach in recInfoDto.AttachmentList)
                {
                    DownloadOSSFile(attach.Url, localPath, attach.AttachName.Trim());
                    hasAttach = true;
                }
            }
            if (!hasAttach)
            {
                return Json(new { ErrorMsg = "没有报备附件.", Status = false, resultCode = 0 });
            }
            //ZIP 
            string zipFile = Path.Combine(downloads, timestamp + ".zip");
            ZipFile.CreateFromDirectory(rootPath, zipFile);
            //Directory.Delete(rootPath, true);//打包后删除原文件夹

            return Json(new { ZipFile = zipFile.Replace(_environment.WebRootPath, ""), Status = true, resultCode = 0 });
        }


        private async Task<IEnumerable<AppealListDto>> SearchApplealInfoList(string sdate, string edate, string sourceType, string carId, string areaId,
            string zoneId, string disId, string appealResult)
        {
            string url = CommonHelper.Current.GetAPIBaseUrl + "/AppealMng/SearchApplealInfoList?";
            
            url += "sdate=" + sdate;
            url += "&edate=" + edate;
            url += "&sourceType=" + sourceType;
            url += "&carId=" + carId;
            url += "&areaId=" + areaId;
            url += "&zoneId=" + zoneId;
            url += "&disId=" + disId;
            url += "&appealResult=" + appealResult;

            string result = await CommonHelper.GetHttpClient().GetStringAsync(url);

            var apiResult = CommonHelper.DecodeString<APIResult>(result);
            IEnumerable<AppealListDto> recItemDto = null;

            if (apiResult.ResultCode == ResultType.Success)
            {
                recItemDto = CommonHelper.DecodeString<IEnumerable<AppealListDto>>(apiResult.Body);
            }

            return recItemDto;
        }

        private async Task<AppealInfo> GetAppealInfoSearch(string aPId)
        {
            string result = await CommonHelper.GetHttpClient().GetStringAsync(CommonHelper.Current.GetAPIBaseUrl + "/AppealMng/AppealInfoSearch?aPId=" + aPId);

            var apiResult = CommonHelper.DecodeString<APIResult>(result);
            AppealInfo recInfoDto = null;

            if (apiResult.ResultCode == ResultType.Success)
            {
                recInfoDto = CommonHelper.DecodeString<AppealInfo>(apiResult.Body);
            }

            return recInfoDto;
        }

        public void DownloadOSSFile(string fileName, string localPath, string attachName)
        {
            byte[] fileData = CommonHelper.Current.HttpGetFileBytes(fileName).Result;
            string filePath = Path.Combine(localPath, attachName);
            BytesToFile(fileData, filePath);
        }


        /// <summary>   
        /// 将 Stream 写入文件   
        /// </summary>   
        public void BytesToFile(byte[] bytes, string fileName)
        {
            FileStream fs = null;
            BinaryWriter bw = null;
            //把 byte[] 写入文件  
            try
            {
                fs = new FileStream(fileName, FileMode.Create);
                fs.Write(bytes, 0, bytes.Length);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                if (fs != null)
                {
                    fs.Dispose();
                }
                if (bw != null)
                {
                    bw.Dispose();
                }
            }
        }
    }
}
