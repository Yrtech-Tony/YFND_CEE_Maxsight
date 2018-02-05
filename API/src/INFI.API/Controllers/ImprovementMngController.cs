using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using INFI.API.Models;
using INFI.API.Models.ImprovementDto;
using INFI.API.Service;
using System.Threading.Tasks;

namespace INFI.API.Controllers
{
    [Authorize]
    [Route("infi/api/v1/[controller]/[action]")]
    public class ImprovementMngController : Controller
    {
        IImprovementService _improvementService;
        public ImprovementMngController(IImprovementService improvementService)
        {
            _improvementService = improvementService;
        }

        [HttpGet]
        [ActionName("GeImprovementItemFromScoreList")]
        public async Task<IActionResult> GeImprovementItemFromScoreList(string itemName, string sDate, string eDate, int inUserId, string statusType, string status, int disId,int depId,int planId,string sourceType)
        {
            var improveDtoList = await _improvementService.SearchImprovementItemFromScoreList(itemName, sDate, eDate, inUserId, statusType, status, disId, depId,planId, sourceType);
            return Ok(improveDtoList);
        }
        [HttpGet]
        [ActionName("GetImpItemFromScore")]
        public async Task<IActionResult> GetImpItemFromScoreList(string sDate, string eDate, string tcKind, string sourceType, string itemName, string aStatus, string pStatus, string rStatus, string sDisId, string inUserId, string userType)
        {
            var improveDtoList = await _improvementService.SearchImpItemFromScoreList(sDate,eDate,tcKind,sourceType,itemName,aStatus,pStatus,rStatus,sDisId,inUserId,userType);
            return Ok(improveDtoList);
        }
        [HttpPost]
        [ActionName("InsertAllocateImprovementItem")]
        public async Task<IActionResult> InsertAllocateImprovementItem([FromBody]ImprovementParamDto paramDto)
        {
            var saveResult = await _improvementService.SaveAllocateImprovementItem(paramDto);
            return Ok(saveResult);
        }
        [HttpPost]
        [ActionName("SaveImprovementItem")]
        public async Task<IActionResult> SaveImprovementItem([FromBody]ImpApprovalParamDto paramDto)
        {
            var saveResult = await _improvementService.SaveImprovementItem(paramDto);
            return Ok(saveResult);
        }

        [HttpPost]
        [ActionName("SaveImprovementResult")]
        public async Task<APIResult> SaveImprovementResult([FromBody]ImprovementResultSaveParamDto paramDto)
        {
            return await _improvementService.SaveImprovementResult(paramDto);
        }
        [HttpGet]
        [ActionName("GetImpPlanOrResultDetail")]
        public async Task<APIResult> GetImpPlanOrResultDetail(string improvementId, string searchType, string impResultId, string tPId, string itemId)
        {
            return await _improvementService.SearchImpPlanOrResultDetail(improvementId, searchType, impResultId, tPId, itemId);
        }
        [HttpGet]
        [ActionName("GetAllTaskOfPlanForImp")]
        public async Task<APIResult> GetAllTaskOfPlanForImp(int inUserId)
        {
            return await _improvementService.SearchAllTaskOfPlanForImp(inUserId);
        }
    }
}
