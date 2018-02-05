using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using INFI.API.Models;
using INFI.API.Service;
using INFI.API.Models.StatisticDto;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace INFI.API.Controllers
{
    [Authorize]
    [Route("infi/api/v1/[controller]/[action]")]
    public class StatisticController : Controller
    {
        public IStatisticService _statisticService;
        public StatisticController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }
        // GET: api/values  根据登录人查询区域
        [HttpGet]
        [ActionName("GetAreaByUserId")]
        public Task<APIResult> GetAreaByUserId(string UserId)
        {
            return _statisticService.GetAreaByUserId(UserId);
        }
        [HttpGet]
        [ActionName("GetImpItems")]
        public Task<APIResult> GetImpItemProcessList(string sDate,
                                                     string eDate,
                                                     int areaId,
                                                     string disId,
                                                     string itemName,
                                                     string allocate,
                                                     string statusType,
                                                     string status,
                                                     int inUserId)
        {
            return _statisticService.GetImpItemProcessList(sDate,
                                                           eDate,
                                                           areaId,
                                                           disId,
                                                           itemName,
                                                           allocate,
                                                           statusType,
                                                           status,
                                                           inUserId);

        }
        // GET: api/values  根据大区查询区域
        [HttpGet]
        [ActionName("GetZoneByAreaId")]
        public Task<APIResult> GetZoneByAreaId(string AreaId)
        {
            return _statisticService.GetZoneByAreaId(AreaId);
        }

        // GET: api/values  查询区域经理巡查进度
        [HttpGet]
        [ActionName("GetPatrolData")]
        public Task<APIResult> GetPatrolData(string SDate, string EDate, string Area,string Zone,string UserId)
        {
            return _statisticService.GetPatrolData(SDate,EDate,Area,Zone,UserId);
        }

        // GET: api/values  根据区域查询经销商
        [HttpGet]
        [ActionName("GetDistributorByAreaId")]
        public Task<APIResult> GetDistributorByAreaId(string AreaId,string UserId,string DisName)
        {
            return _statisticService.GetDistributorByAreaId(AreaId, UserId, DisName);
        }

        // GET: api/values  根据登录名查询有权限的经销商
        [HttpGet]
        [ActionName("GetDisListByUserId")]
        public Task<APIResult> GetDisListByUserId( string UserId)
        {
            return _statisticService.GetDisListByUserId(UserId);
        }

        // GET: api/values  根据根据经销商获取信息
        [HttpGet]
        [ActionName("GetInfoByDisId")]
        public Task<APIResult> GetInfoByDisId(string DisId)
        {
            return _statisticService.GetInfoByDisId(DisId);
        }

        // GET: api/values   售后数据查询
        [HttpGet]
        [ActionName("GetAftersalesFigures")]
        public Task<APIResult> GetAftersalesFigures(string DisId,string YearMonthCSS, string YearMonthCCM, string YearMonthB)
        {
            return _statisticService.GetAftersalesFigures(DisId,YearMonthCSS,YearMonthCCM,YearMonthB);
        }

        // GET: api/values   售后数据查询(HighCharts)
        [HttpGet]
        [ActionName("GetAftersalesFiguresForHighCharts")]
        public Task<APIResult> GetAftersalesFiguresForHighCharts(string DisId, string Year)
        {
            return _statisticService.GetAftersalesFiguresForHighCharts(DisId, Year);
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        // POST api/values  经营数据报告EXCEL的导入
        [HttpPost]
        [ActionName("InsertAfterSalesDataByExcel")]
        public Task<APIResult> InsertAfterSalesDataByExcel([FromBody]ExcelAfterSalesDataParams param)
        {

            return _statisticService.InsertAfterSalesDataByExcel(param.InUserId,param.AfterSalesData);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
