using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using INFI.API.Models;
using INFI.API.Service;
using INFI.API.Models.RecordDto;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace INFI.API.Controllers
{
    [Authorize]
    [Route("infi/api/v1/[controller]/[action]")]
    public class RecordController : Controller
    {
        public IRecordService _recordService;
        public RecordController(IRecordService recordService)
        {
            _recordService = recordService;
        }

        [HttpGet]
        [ActionName("GetRecordInfoList")]
        public Task<APIResult> GetRecordInfoList(int userId, string recordType, string recordSDate, string recordEDate, int DisId, string approvalStatus)
        {
            return _recordService.GetRecordInfoList(userId, recordType, recordSDate, recordEDate, DisId, approvalStatus);
        }
        // POST api/values
        [HttpPost]
        [ActionName("RecordInfoReg")]
        public Task<APIResult> RecordInfoReg([FromBody]RecordInfoDto param)
        {
            return _recordService.RecordInfoReg(param);
        }
        [HttpGet]
        [ActionName("RecordInfoSearch")]
        public Task<APIResult> RecordInfoSearch(int rId)
        {
            return _recordService.RecordInfoSearch(rId);
        }
        //报备审核
        [HttpPost]
        [ActionName("UpdateRecordInfo")]
        public Task<APIResult> UpdateRecordInfo([FromBody]UpdateRecordInfoParam param)
        {
            return _recordService.UpdateRecordInfo(param.UserId, param.Id, param.ApprovalStatus, param.ApprovalRemark, param.ApproalAttachList);
        }
    }
}
