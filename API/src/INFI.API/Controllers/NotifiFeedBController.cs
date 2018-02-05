using Microsoft.AspNetCore.Mvc;
using INFI.API.Service;
using System.Threading.Tasks;
using System;
using INFI.API.Models;
using INFI.API.Models.NoticeFeedBackDto;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace INFI.API.Controllers
{
    [Authorize]
    [Route("infi/api/v1/[controller]/[action]")]
    public class NotifiFeedBController : Controller
    {
        public INotifiFeedBService _notifiFeedBService;
        public NotifiFeedBController(INotifiFeedBService notifiFeedBService)
        {
            _notifiFeedBService = notifiFeedBService;
        }
        // GET: api/values
        //brands/TT/products?type=11
        [HttpGet]
        public  Task<APIResult> Get()
        {
           string type= Request.Query["Type"];
            if (type == "Mst")
            {
                string SDate = Request.Query["SDate"];
                string EDate = Request.Query["EDate"];
                int UserID = Convert.ToInt32(Request.Query["UserID"]);
                string ReplyYN = Request.Query["ReplyYN"];
                string ApprovalStatus = Request.Query["ApprovalStatus"];
                string NoticeNo = Request.Query["NoticeNo"];
                string Title = Request.Query["Title"];
                return  _notifiFeedBService.sreachNotiFeedBMstList(SDate, EDate, UserID, ReplyYN, ApprovalStatus, NoticeNo, Title);
                
            }
            else if (type == "Dtl")
            {
                int UserID = Convert.ToInt32(Request.Query["UserID"]);
                int NoticeReaderId = Convert.ToInt32(Request.Query["NoticeReaderId"]);
                return  _notifiFeedBService.sreachNotiFeedBDtlList(UserID, NoticeReaderId);
                
            }
            else if (type == "Attachment")
            {
                int UserID = Convert.ToInt32(Request.Query["UserID"]);
                int NoticeReaderId = Convert.ToInt32(Request.Query["NoticeReaderId"]);
                return  _notifiFeedBService.sreachNotiFeedBAttachMentList(UserID, NoticeReaderId);
                
            }
            else {
                return null;
            }
            
        }

        // GET: api/values  需要反馈的List查询
        [HttpGet]
        [ActionName("searchNotiFeedBMstListByUserId")]
        public Task<APIResult> searchNotiFeedBMstListByUserId(string UserId)
        {
            return _notifiFeedBService.searchNotiFeedBMstListByUserId(UserId);
        }
        // GET: api/values  需要反馈的List查询Web
        [HttpGet]
        [ActionName("searchNoticeFeedbackList")]
        public Task<APIResult> searchNoticeFeedbackList(string SDate, string EDate, string UserID, string FeedBackYN, string ApprovalStatus, string NoticeNo, string Title)
        {
            return _notifiFeedBService.searchNoticeFeedbackList(SDate,EDate,UserID,FeedBackYN,ApprovalStatus,NoticeNo,Title);
        }

        // GET: api/values  反馈详细内容
        [HttpGet]
        [ActionName("SearchNoticeFeedBackDtl")]
        public Task<APIResult> searchNoticeFeedBackDtl(string NoticeId ,string UserId,string DisId,string DepartId)
        {
            return _notifiFeedBService.searchNoticeFeedBackDtl(NoticeId,UserId,DisId,DepartId);
        }
        // POST api/values
        [HttpPost]
        [ActionName("NotiFeedBackS")]
        public async Task<APIResult> Post([FromBody]NotiFeedBackSaveParams param)
        {
            return await _notifiFeedBService.NotiFeedBackS(param.NoticeId,param.UserId,param.ReplyContent,param.Status,param.XmlData);

        }

        // POST api/values  任务卡的制作
        [HttpPost]
        [ActionName("SaveFeedBackNotice")]
        public Task<APIResult> SaveFeedBackNotice([FromBody]FeedBackInfoDto param)
        {

            return _notifiFeedBService.SaveFeedBackNotice(param.NoticeId, param.UserId, param.ReplyContent, param.Status, param.list);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
