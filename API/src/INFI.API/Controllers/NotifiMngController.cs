using Microsoft.AspNetCore.Mvc;
using INFI.API.Service;
using INFI.API.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using INFI.API.Models.CasesInfo;
using INFI.API.Models.NotifiMngDto;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace INFI.API.Controllers
{
    [Authorize]
    [Route("infi/api/v1/[controller]/[action]")]
    public class NotifiMngController : Controller
    {
        public INotifiMngService _notifiMngService;
        public NotifiMngController(INotifiMngService notifiMngService)
        {
            _notifiMngService = notifiMngService;
        }
        // GET: api/values
        [HttpGet]
        [ActionName("Search")]
        public async Task<APIResult> Get()
        {
            if (Request.Query.Count > 1)
            {
                string fromDate = Request.Query["fromDate"];
                string toDate = Request.Query["toDate"];
                string noticeReaders = Request.Query["noticeReaders"];
                string status = Request.Query["status"];
                string needReply = Request.Query["needReply"];
                string title = Request.Query["title"];
                string noticeNo = Request.Query["noticeNo"];
                string inUserId = Request.Query["inUserId"];
                return await _notifiMngService.SearchMadeNoticeList(fromDate,
                                                              toDate,
                                                              noticeReaders,
                                                              status,
                                                               needReply,
                                                               title,
                                                               noticeNo,
                                                               inUserId);
            }
            else
            {
                return await _notifiMngService.SearchMadeNoticeDetailInfo(Request.Query["id"]);
            }
        }

        [HttpPost]
        [ActionName("SearchLength")]
        public async Task<APIResult> SearchMadeNoticeList([FromBody]SearchParamDto searchParamDto)
        {
            return await _notifiMngService.SearchMadeNoticeList(searchParamDto.FromDate,
                                                                searchParamDto.ToDate,
                                                               searchParamDto.NoticeReaders,
                                                               searchParamDto.Status,
                                                               searchParamDto.NeedReply,
                                                               searchParamDto.Title,
                                                               searchParamDto.NoticeNo,
                                                               searchParamDto.InUserId);
        }
        // POST api/values
        [HttpPost]
        [ActionName("Save")]
        public async Task<APIResult> SaveNoticeMade([FromBody]NoticeInfoDto noticeInfoDto)
        {
            return await _notifiMngService.SaveNoticeMade(noticeInfoDto);
        }

        // PUT api/values/5
        [HttpPost]
        [ActionName("Delete")]
        public async Task<APIResult> UpdateMadeNoticeList([FromBody]CasesDelParamDto paramDto)
        {
           
            return await _notifiMngService.UpdateMadeNoticeList(paramDto);
           
        }

        [HttpGet]
        [ActionName("GetList")]
        public async Task<APIResult> GetReaders()
        {
            return await _notifiMngService.SearchNoticeReaders(Request.Query["noticeId"], Request.Query["inUserId"]);
        }
        // PUT api/values/5
        [HttpPost]
        [ActionName("Update")]
        public async Task<APIResult>  UpdateReaderReadStatus([FromBody]NoticeReadStatusDto paramDto)
        {

            return await _notifiMngService.UpdateReaderReadStatus(paramDto);

        }
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            
        }

        [HttpGet]
        [ActionName("SearchDis")]
        public async Task<APIResult> SearchDisReader()
        {
            return await _notifiMngService.GetNoticeDisReader(Request.Query["NoticeId"]);
        }
    }
}
