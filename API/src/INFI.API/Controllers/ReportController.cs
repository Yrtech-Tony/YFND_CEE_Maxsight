using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using INFI.API.Service;
using INFI.API.Models;
using INFI.API.Models.ReportDto;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace INFI.API.Controllers
{
    [Authorize]
    [Route("infi/api/v1/[controller]/[action]")]
    public class ReportController : Controller
    {
        IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }
        [HttpGet]
        [ActionName("GetAttachmentByUserId")]
        public Task<APIResult> GetAttachmentByUserId(int userId, string sourceType, string sDate, string eDate)
        {
            return _reportService.GetAttachmentByUserId(userId, sourceType, sDate, eDate);
        }
        [HttpPost]
        [ActionName("SaveReportAttachment")]
        public Task<APIResult> SaveReportAttachment([FromBody]SaveReportAttachdto param)
        {
            return _reportService.SaveReportAttachment(param);
        }
        [HttpPost]
        [ActionName("UpdateAttachmentDownloadCnt")]
        public Task<APIResult> UpdateAttachmentDownloadCnt([FromBody]int id)
        {
            return _reportService.UpdateAttachmentDownloadCnt(id);
        }

        [HttpGet]
        [ActionName("GetPlansListForExcelDownload")]
        public Task<APIResult> GetPlansListForExcelDownload(string SDate, string EDate, string UserId, string DisId)
        {
            return _reportService.GetPlansListForExcelDownload(SDate, EDate, UserId, DisId);
        }

        [HttpGet]
        [ActionName("GetRegion")]
        public Task<APIResult> GetRegionByUserId(int inuserid, string usertype, string zonetype)
        {
            return _reportService.GetRegionByUserId(inuserid, usertype, zonetype);
        }
        [HttpGet]
        [ActionName("GetArea")]
        public Task<APIResult> GetAreaByRegionId(int inuserid, int regionid, string usertype)
        {
            return _reportService.GetAreaByRegionId(inuserid, regionid, usertype);
        }
    }
}
