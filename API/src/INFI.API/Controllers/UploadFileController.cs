using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using INFI.API.Service;
using INFI.API.Models.AttachmentMngDto;
using Microsoft.AspNetCore.Authorization;

namespace INFI.API.Controllers
{
    [Authorize]
    [Route("infi/api/v1/[controller]")]
    public class UploadFileController :Controller
    {
        IUploadFileService _uploadFileService;
        public UploadFileController(IUploadFileService uploadFileService)
        {
            _uploadFileService = uploadFileService;
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var fruitDtoList = await _uploadFileService.FruitQuery();
            return Ok(fruitDtoList);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] DocumentInput upload)
        {
            var result = await _uploadFileService.Create(upload);
            return Ok(result);
        }
    }
}
