using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using INFI.API.Service;
using INFI.API.Models;
using INFI.API.Models.CalenderMngDto;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace INFI.API.Controllers
{
    [Authorize]
    [Route("infi/api/v1/[controller]/[action]")]
    public class CalenderMngController : Controller
    {
        public ICalenderMngService _calenderMngService;
        public CalenderMngController(ICalenderMngService calenderMngService)
        {
            _calenderMngService = calenderMngService;
        }

        // GET: api/values 查询当月登陆者所有的日历任务
        [HttpGet]
        [ActionName("getCalenderListAll")]
        public Task<APIResult> GetCalenderListAll(string UserId,string Date)
        {
            return _calenderMngService.GetCalenderListAll(UserId,Date);
        }

        // GET: api/values 查询当月登陆者所有的日历任务
        [HttpGet]
        [ActionName("GetCalenderListAllWeb")]
        public Task<APIResult> GetCalenderListAllWeb(string UserId, string SDate, string EDate)
        {
            return _calenderMngService.GetCalenderListAllWeb(UserId, SDate, EDate);
        }

        // GET: api/values 查询当月登陆者所有的日历任务
        [HttpGet]
        [ActionName("GetCalenderListAllWebR02")]
        public Task<APIResult> GetCalenderListAllWebR02(string UserId, string Date)
        {
            return _calenderMngService.GetCalenderListAllWebR02(UserId, Date);
        }

        // POST api/values 创建/修改日历任务
        [HttpPost]
        [ActionName("CreateCalenderPlans")]
        public async Task<APIResult> CreateCalenderPlans([FromBody]SaveCalenderMngParams param)
        {
            return await _calenderMngService.CreateCalenderPlans(param.Id, param.Title, param.Content, param.Type, param.SDate,param.EDate,param.UserID);
        }

        // POST api/values 删除日历任务
        [HttpPost]
        [ActionName("DeleteCalenderPlans")]
        public async Task<APIResult> DeleteCalenderPlans([FromBody]string  Id)
        {
            return await _calenderMngService.DeleteCalenderPlans(Id);
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
