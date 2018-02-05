using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using INFI.API.Service;

namespace INFI.API.Controllers
{
    [Route("infi/api/v1/[controller]")]
    public class AppInfoController : Controller
    {
        IAppInfoService _appInfoService;
        public AppInfoController(IAppInfoService appInfoService)
        {
            _appInfoService = appInfoService;
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var fruitDtoList = await _appInfoService.FruitQuery();
            return Ok(fruitDtoList);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
    }
}
