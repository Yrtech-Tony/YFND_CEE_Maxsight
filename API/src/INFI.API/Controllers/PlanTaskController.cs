using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using INFI.API.Models;
using INFI.API.Service;
using INFI.API.Models.PlanTaskMngDto;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;


// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace INFI.API.Controllers
{
    [Authorize]
    [Route("infi/api/v1/[controller]/[action]")]
    public class PlanTaskController : Controller
    {
        public IPlanTaskMngService _planTaskMngService;

        public PlanTaskController(IPlanTaskMngService planTaskMngService)
        {
            _planTaskMngService = planTaskMngService;
        }


        // GET: api/values  计划任务列表查询
        [HttpGet]
        [ActionName("GetPlansList")]
        public Task<APIResult> GetPlansList(string Title, string VisitType, string SDate, string EDate,string UserId,string SourceType)
        {
            return _planTaskMngService.GetPlansList(Title,VisitType,SDate,EDate,UserId, SourceType);
        }

        // GET: api/values  计划任务详细查询
        [HttpGet]
        [ActionName("GetPlansDtlList")]
        public Task<APIResult> GetPlansDtlList(string Id)
        {
            return _planTaskMngService.GetPlansDtlList(Id);
        }

        // GET: api/values  //获取拜访类型
        [HttpGet]
        [ActionName("GetStatus")]
        public Task<APIResult> GetStatus(string GroupCode)
        {
            return _planTaskMngService.GetStatus(GroupCode);
        }

        // GET: api/values  //获取当前用户的区域
        [HttpGet]
        [ActionName("GetDistributorByUserId")]
        public Task<APIResult> GetDistributorByUserId(string UserId)
        {
            return _planTaskMngService.GetDistributorByUserId(UserId);
        }
        // GET: api/values  //获取所有的经销商
        [HttpGet]
        [ActionName("GetAllDistributor")]
        public Task<APIResult> GetAllDistributor(string Type,string Code,string Name,string DisId)
        {
            return _planTaskMngService.GetAllDistributor(Type,Code,Name,DisId);
        }


        // GET: api/values  //获取任务卡
        [HttpGet]
        [ActionName("GetAllTaskCard")]
        public Task<APIResult> GetAllTaskCard(string Type, string TCRang, string SDate,string EDate,string userid,string dislist)
        {

            List<StatusDto> list = new List<StatusDto>();
            if (dislist != null)
            {

                for (int i = 0; i < dislist.Split(',').Length; i++)
                {
                    StatusDto dto = new StatusDto();
                    dto.Value = dislist.Split(',')[i];
                    list.Add(dto);
                }
            }
            return _planTaskMngService.GetAllTaskCard(Type,TCRang,SDate,EDate,userid,list);
        }


        // GET: api/values  计划任务审核列表查询
        [HttpGet]
        [ActionName("GetReviewPlansList")]
        public Task<APIResult> GetReviewPlansList(string Title, string VisitType, string SDate, string EDate,string PStatus,string UserId,string Area)
        {
            return _planTaskMngService.GetReviewPlansList(Title, VisitType, SDate, EDate, PStatus,UserId,Area);
        }
        // GET: api/values  计划任务审核列表查询Mobile
        [HttpGet]
        [ActionName("GetReviewPlansList_Mobile")]
        public Task<APIResult> GetReviewPlansList_Mobile(string UserId)
        {
            return _planTaskMngService.GetReviewPlansList_Mobile( UserId);
        }

        // GET: api/values  计划任务制作加载必选任务
        [HttpGet]
        [ActionName("GetMandatoryPlansDtlById")]
        public Task<APIResult> GetMandatoryPlansDtlById(string UserId,string XMLdata)
        {
            List<StatusDto> list = new List<StatusDto>();
            if (XMLdata != null)
            {
                
                for (int i = 0; i < XMLdata.Split(',').Length; i++)
                {
                    StatusDto dto = new StatusDto();
                    dto.Value = XMLdata.Split(',')[i];
                    list.Add(dto);
                }
            }          
            return _planTaskMngService.GetMandatoryPlansDtlById(UserId,list);
        }

        // GET: api/values  计划任务审核状态查询
        [HttpGet]
        [ActionName("GetReviewStatusByGroupCode")]
        public Task<APIResult> GetReviewStatusByGroupCode(string GroupCode)
        {
            return _planTaskMngService.GetReviewStatusByGroupCode(GroupCode);
        }

        // GET: api/values  标准任务卡查询查询
        [HttpGet]
        [ActionName("GetTaskCardList")]
        public Task<APIResult> GetTaskCardList(string SDate, string EDate, string TCType, string TCRange, string InUserId,string UseYN,string SourceType,string Kind)
        {
            return _planTaskMngService.GetTaskCardList(SDate,EDate,TCType,TCRange,InUserId,UseYN,SourceType,Kind);
        }

        // GET: api/values  标准任务卡查询查询
        [HttpGet]
        [ActionName("GetTaskCardDtlList")]
        public Task<APIResult> GetTaskCardDtlList(string TCId)
        {
            return _planTaskMngService.GetTaskCardDtlList(TCId);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values  计划任务审核
        [HttpPost]
        [ActionName("ReviewPlans")]
        public Task<APIResult> ReviewPlans([FromBody]ReviewPlansParams param)
        {
            return  _planTaskMngService.ReviewPlans(param.Id, param.PStatus, param.RejectReason, param.UserId);
        }

        // POST api/values  计划任务的制作
        [HttpPost]
        [ActionName("AddorUpdatePlans")]
        public Task<APIResult> AddorUpdatePlans([FromBody]AddorUpdatePlansPatams param)
        {
            return _planTaskMngService.AddorUpdatePlans(param.PId, param.Title, param.DistributorId, param.VisitDateTime, param.VisitType, param.PStatus, param.InUserId, param.XmlData);
        }

        // POST api/values 创建计划任务
        [HttpPost]
        [ActionName("CreatePlans")]
        public Task<APIResult> CreatePlans([FromBody]AddorUpdatePlansPatams param)
        {
            List<StatusDto> list = new List<StatusDto>();
            if (param.list != null)
            {

                for (int i = 0; i < param.list.Split(',').Length; i++)
                {
                    StatusDto dto = new StatusDto();
                    dto.Value = param.list.Split(',')[i];
                    list.Add(dto);
                }
            }

            return _planTaskMngService.CreatePlans(param.PId, param.Title, param.DistributorId, param.VisitDateTime, param.VisitType, param.PStatus, param.InUserId, param.XmlData,list);
        }

        // POST api/values  计划任务详细的删除
        [HttpPost]
        [ActionName("DeleteTaskOfPlans")]
        public Task<APIResult> DeleteTaskOfPlans([FromBody]ReviewPlansParams param)
        {
            return _planTaskMngService.DeleteTaskOfPlans(param.Id);
        }


        // POST api/values  计划任务关闭
        [HttpPost]
        [ActionName("ClosePlanById")]
        public Task<APIResult> ClosePlanById([FromBody]ReviewPlansParams param)
        {
            return _planTaskMngService.ClosePlanById(param.Id);
        }


        // POST api/values  任务卡的制作
        [HttpPost]
        [ActionName("CreateTaskCard")]
        public Task<APIResult> CreateTaskCard([FromBody]AddTaskCardParams param)
        {

            return _planTaskMngService.AddTaskCard(param.TCType,param.TCRange,param.TCTitle,param.SourceType,param.TCDescription,param.InUserId,param.Kind,param.TaskItem,param.XmlDataS,param.XmlDataP);    
        }



        // POST api/values  任务卡的修改作
        [HttpPost]
        [ActionName("UpdateTaskCard")]
        public Task<APIResult> UpdateTaskCard([FromBody]AddTaskCardParams param)
        {

            return _planTaskMngService.UpdateTaskCard(param.Id,param.TCType, param.TCRange, param.TCTitle, param.TCDescription, param.InUserId,param.UseYN,param.Kind ,param.TaskItem, param.XmlDataS, param.XmlDataP,param.DelTIList,param.DelPSList,param.DelCSList);
        }

        // POST api/values  Excel导入计划任务
        [HttpPost]
        [ActionName("ExcelUploadPlans")]
        public Task<APIResult> ExcelUploadPlans([FromBody]ExcelUploadPlansPatams param)
        {

            return _planTaskMngService.ExcelUploadPlans(param.InUserId,param.SourceType,param.Plans, param.TaskOfPlans, param.TaskCard, param.TaskItem, param.CheckStandard, param.Score, param.CheckResult,param.PictureStandard);
        }

        [HttpPost]
        [ActionName("PlansPush")]
        public Task<APIResult> PlansPush([FromBody]PlansPushPatams param)
        {

            return _planTaskMngService.PlansPush(param.Id,param.UserId,param.Content);
        }

        //手机获取Push信息
        [HttpGet]
        [ActionName("GetPushInfoForMobile")]
        public Task<APIResult> GetPushInfoById(string Id)
        {
            return _planTaskMngService.GetPushInfoById(Id);
        }

        //经销商推送方式内容查询
        [HttpGet]
        [ActionName("GetPushInfoForPlanId")]
        public Task<APIResult> GetPushInfoForPlanId(string PId)
        {
            return _planTaskMngService.GetPushInfoByPlanId(PId);
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
