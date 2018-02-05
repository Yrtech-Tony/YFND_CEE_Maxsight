using Microsoft.AspNetCore.Mvc;
using INFI.API.Service;
using System.Threading.Tasks;
using INFI.API.Models.NoticeApproal;
using INFI.API.Models;
using System;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace INFI.API.Controllers
{
    [Authorize]
    [Route("infi/api/v1/[controller]/[action]")]
    public class NotifiApprovalController : Controller
    {
        INotifiApprovalService _notifiApprovalService;
        public NotifiApprovalController(INotifiApprovalService notifiApprovalService)
        {
            _notifiApprovalService = notifiApprovalService;
        }

        [HttpGet]
        [ActionName("GetNoticeApprovalLog")]
        public Task<APIResult> GetNoticeApprovalLog(int noticeReaderId)
        {
            return _notifiApprovalService.GetNoticeApprovalLog(noticeReaderId);
        }

        [HttpPost]
        [ActionName("NoticeApprovalS")]
        public Task<APIResult> NoticeApprovalS([FromBody]NeedApproalParams param)
        {
            return _notifiApprovalService.NoticeApprovalS(param);
        }
        [HttpGet]
        [ActionName("GetNoticeDepartments")]
        public Task<APIResult> GetNoticeDepartments()
        {
            return _notifiApprovalService.GetNoticeDepartments();
        }
        [HttpGet]
        [ActionName("GetApprovalStatus")]
        public Task<APIResult> GetApprovalStatus()
        {
            return _notifiApprovalService.GetApprovalStatus();
        }
        [HttpGet]
        [ActionName("GetNeedApprovalDtoList")]
        public Task<APIResult> GetNeedApprovalDtoList(string approvalStatus, string sDate, string eDate, string noticeNo, string noticeDepartment, int userId)
        {
            // 审批List
            return _notifiApprovalService.GetNeedApprovalDtoList(approvalStatus, sDate, eDate, noticeNo, noticeDepartment, userId);
        }
        [HttpGet]
        [ActionName("GetNeedApprovalDtoList2")]
        public Task<APIResult> GetNeedApprovalDtoList2(int userId)
        {
            // 审批List
            return _notifiApprovalService.GetNeedApprovalDtoList2(userId);
        }
        [HttpGet]
        [ActionName("GetNoticeApprovalDetail")]
        public Task<APIResult> GetNoticeApprovalDetail(int noticeReaderId)
        {
            //查询通知审核详细
            return _notifiApprovalService.GetNoticeApprovalDetail(noticeReaderId);
        }
        [HttpGet]
        [ActionName("GetDistributorListByUserId")]
        public Task<APIResult> GetDistributorListByUserId(int UserId, string disCode="%", string disName="%")
        {
            return _notifiApprovalService.GetDistributorListByUserId(UserId,  disCode,  disName);
        }
        [HttpGet]
        [ActionName("GetApprovalNoticeList")]
        public Task<APIResult> GetApprovalNoticeList(int userId, string status, string sDate, string eDate, string noticeNo)
        {
            return _notifiApprovalService.GetApprovalNoticeList(userId, status,  sDate,  eDate,  noticeNo);
        }
        [HttpGet]
        [ActionName("GetApprovalReaderList")]
        public Task<APIResult> GetApprovalReaderList(int noticeId, int userId)
        {
            return _notifiApprovalService.GetApprovalReaderList(noticeId, userId);
        }
    }
}
