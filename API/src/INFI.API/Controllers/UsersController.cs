using Microsoft.AspNetCore.Mvc;
using INFI.API.Service;
using System.Threading.Tasks;
using INFI.API.Models;
using Microsoft.AspNetCore.Authorization;
using INFI.API.Models.UsersDto;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace INFI.API.Controllers
{
    [Authorize]
    [Route("infi/api/v1/[controller]/[action]")]
    public class UsersController : Controller
    {
        public IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        // GET: api/values
        [HttpGet]
        [ActionName("Login")]
        public Task<APIResult> Get(string UserName, string Password)
        {
            return _usersService.Login(UserName, Password);
        }
        [HttpGet("{UserName}/{Password}")]
        [ActionName("GetForBs")]
        public Task<APIResult> GetForBs(string UserName, string Password)
        {
            return _usersService.LoginForBs(UserName, Password);
        }
        [HttpGet]
        [ActionName("GetEmployeeInfo")]
        public Task<APIResult> GetEmployeeInfo(string DisId, string DepartId, string UserType, string UserName, string UseYN)
        {
            return _usersService.GetEmployeeInfo(DisId, DepartId, UserType, UserName, UseYN);
        }
        [HttpPost]
        [ActionName("SaveEmployeeInfo")]
        public Task<APIResult> SaveEmployeeInfo([FromBody]ParamEmpInfoDto param)
        {
            return _usersService.SaveEmployeeInfo(param);
        }
        [HttpPost]
        [ActionName("UpdatePsw")]
        public Task<APIResult> UpdatePsw([FromBody]ParamEmpPswDto param)
        {
            return _usersService.UpdatePsw(param);
        }
        [HttpGet]
        [ActionName("GetDistributorInfo")]
        public Task<APIResult> GetDistributorInfo(string DisId, string UseYN)
        {
            return _usersService.GetDistributorInfo(DisId, UseYN);
        }
        [HttpPost]
        [ActionName("SaveDistributorInfo")]
        public Task<APIResult> SaveDistributorInfo([FromBody]ParamDisInfoDto disInfoDto)
        {
            return _usersService.SaveDistributorInfo(disInfoDto);
        }
        [HttpGet]
        [ActionName("GetOrgInfo")]
        public Task<APIResult> GetOrgInfo(string UserId)
        {
            return _usersService.GetOrgInfo(UserId);
        }
        [HttpGet]
        [ActionName("GetPushInfo")]
        public Task<APIResult> GetPushInfo()
        {
            return _usersService.GetPushInfo();
        }
        [HttpGet]
        [ActionName("GetGroupList")]
        public Task<APIResult> GetGroupList()
        {
            return _usersService.GetGroupList();
        }
        [HttpGet]
        [ActionName("GetTypeList")]
        public Task<APIResult> GetTypeList(string groupCode, string name, string useYN)
        {
            return _usersService.GetTypeList(groupCode, name, useYN);
        }
        [HttpPost]
        [ActionName("UpdateType")]
        public Task<APIResult> UpdateType([FromBody]UpdateTypeParam param)
        {
            return _usersService.UpdateType(param.InUserId, param.list);
        }

        [HttpGet]
        [ActionName("GetAllDataForLocalDB")]
        public Task<APIResult> GetAllDataForLocalDB(string UserId)
        {
            return _usersService.GetAllDataForLocalDB(UserId);
        }

        [HttpPost]
        [ActionName("InsertDealers")]
        public Task<APIResult> SaveDealerList([FromBody]ParamDealerDto paramDto)
        {
            return _usersService.SaveDealerList(paramDto);
        }

    }
}
