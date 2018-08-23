using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using INFI.API.Models;
using INFI.API.Service;
using INFI.API.Models.AppealMngDto;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace INFI.API.Controllers
{
    [Authorize]
    [Route("infi/api/v1/[controller]/[action]")]
    public class AppealMngController : Controller
    {

        IAppealMngService _appealMngService;
        public AppealMngController(IAppealMngService appealMngService)
        {
            _appealMngService = appealMngService;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        // GET: api/values 审核列表
        [HttpGet]
        [ActionName("SearchApplealInfoList")]
        public Task<APIResult> SearchApplealInfoList(string sdate, string edate, string sourceType, string carId, string areaId, string zoneId, string disId, string appealResult,string chapterType)
        {
            return _appealMngService.SearchApplealInfoList(sdate,edate,sourceType,carId,areaId,zoneId,disId,appealResult, chapterType);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        //申诉审核
        [HttpPost]
        [ActionName("UpdateApplealInfo")]
        public Task<APIResult> UpdateApplealInfo([FromBody]UpdateAppealInfoParam param)
        {
             return _appealMngService.UpdateApplealInfo(param.UserId,param.Id,param.AppealResult,param.ApprovalRemark,param.ApproalAttachList);
        }

        // POST api/values
        [HttpPost]
        [ActionName("AppealInfoReg")]
        public Task<APIResult> AppealInfoReg([FromBody]AppealInfo param)
        {
            return _appealMngService.AppealInfoReg(param);
        }
        [HttpGet]
        [ActionName("AppealInfoSearch")]
        public Task<APIResult> AppealInfoSearch(int aPId)
        {
            return _appealMngService.AppealInfoSearch(aPId);
        }
        [HttpGet]
        [ActionName("SearchChapterType")]
        public Task<APIResult> SearchChapterType()
        {
            return _appealMngService.SearchChapterType();
        }
    }
}
