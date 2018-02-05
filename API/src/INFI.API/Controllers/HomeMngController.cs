using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using INFI.API.Models;
using INFI.API.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Controllers
{
    [Authorize]
    [Route("infi/api/v1/[controller]/[action]")]
    public class HomeMngController : Controller
    {
        public IHomeMngService _homeMngService;
        public HomeMngController(IHomeMngService homeMngService)
        {
            _homeMngService = homeMngService;
        }

        [HttpGet]
        [ActionName("AllItems")]
        public Task<APIResult> GetAllDoItemList(string UserId, string SearchDate)
        {
            return _homeMngService.GetAllDoItemList(UserId, SearchDate);
        }

        [HttpGet]
        [ActionName("AllItemsForMobile")]
        public Task<APIResult> GetAllDoItemListForMobile(string UserId)
        {
            return _homeMngService.GetAllDoItemListForMobile(UserId);
        }
    }
}
