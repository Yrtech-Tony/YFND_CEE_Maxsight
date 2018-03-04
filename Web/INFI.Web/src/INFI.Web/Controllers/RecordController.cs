using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using INFI.Web.Common;
using INFI.Web.Common.Module;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNetCore.Hosting;
using System.IO.Compression;
using System.Diagnostics;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace INFI.Web.Controllers
{   
    public class RecordController : Controller
    {
        private IHostingEnvironment _environment;
        public RecordController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [PermissionRequired]
        public IActionResult REC010()
        {
            DateTime now = DateTime.Now;
            ViewBag.CurrentDate = now.ToString("yyyy-MM-dd");
            ViewBag.FirstDay = new DateTime(now.Year, now.Month, 1).ToString("yyyy-MM-dd");
            return View();
        }

        [PermissionRequired]
        public IActionResult REC010P()
        {
            return View();
        }

        
        public IActionResult DownloadAttachBatch(int userId, string recordType, string recordSDate, string recordEDate, int DisId, string approvalStatus)
        {
            Task<IEnumerable<RecordItemDto>> result = GetRecordInfoList(userId, recordType, recordSDate, recordEDate, DisId, approvalStatus);

            string downloads = Path.Combine(_environment.WebRootPath, "downloads");
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string rootPath = Path.Combine(downloads, timestamp); 

            bool hasAttach = false;
            foreach(RecordItemDto recItemDao in result.Result)
            {
                int RId = recItemDao.RId;
                string firstDic = recItemDao.DisCode.Trim() +"_"+ recItemDao.DisName.Trim();
                string secondDic = recItemDao.RecordType.Trim() + "_" + recItemDao.RecordTitle.Trim();                
                string localPath = Path.Combine(rootPath, firstDic, secondDic);               

                if (!Directory.Exists(localPath))
                {
                    Directory.CreateDirectory(localPath);
                }
                RecordInfoDto recInfoDto = GetRecordInSearch(RId).Result;
                if(recInfoDto == null || recInfoDto.AttachmentList == null || recInfoDto.AttachmentList.Count==0)
                {
                    continue;
                }
                foreach (AttachmentMngDto attach in recInfoDto.AttachmentList)
                {
                    DownloadOSSFile(attach.Url, localPath, attach.AttachName.Trim());
                    hasAttach = true;
                }
            }
            if (!hasAttach)
            {
                return Json(new { ErrorMsg = "没有报备附件." , Status=false});
            }
            //ZIP 
            string zipFile = Path.Combine(downloads, timestamp + ".zip");
            ZipFile.CreateFromDirectory(rootPath, zipFile);
            Directory.Delete(rootPath, true);//打包后删除原文件夹

            return Json(new { ZipFile = zipFile.Replace(_environment.WebRootPath,""), Status = true });
        }

        private async Task<IEnumerable<RecordItemDto>> GetRecordInfoList(int userId, string recordType, string recordSDate, string recordEDate, int DisId, string approvalStatus)
        {
            string url = CommonHelper.Current.GetAPIBaseUrl + "/Record/GetRecordInfoList?";
            url += "userId=" + userId;
            url += "&recordType=" + recordType;
            url += "&recordSDate=" +  recordSDate;
            url += "&recordEDate=" + recordEDate;
            url += "&DisId=" + DisId;
            url += "&approvalStatus=" + approvalStatus;
            string result = await CommonHelper.GetHttpClient().GetStringAsync(url);

            var apiResult = CommonHelper.DecodeString<APIResult>(result);
            IEnumerable<RecordItemDto> recItemDto = null;

            if (apiResult.ResultCode == ResultType.Success)
            {
                recItemDto = CommonHelper.DecodeString<IEnumerable<RecordItemDto>>(apiResult.Body);
            }

            return recItemDto;
        }

        private async Task<RecordInfoDto> GetRecordInSearch(int rId)
        {
            string result = await CommonHelper.GetHttpClient().GetStringAsync(CommonHelper.Current.GetAPIBaseUrl + "/Record/RecordInfoSearch?rId=" + rId);

            var apiResult = CommonHelper.DecodeString<APIResult>(result);
            RecordInfoDto recInfoDto = null;

            if (apiResult.ResultCode == ResultType.Success)
            {
                recInfoDto = CommonHelper.DecodeString<RecordInfoDto>(apiResult.Body);
            }

            return recInfoDto;
        }

        public void DownloadOSSFile(string fileName,string localPath,string attachName)
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
                fs.Write(bytes,0, bytes.Length);
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
