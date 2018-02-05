using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using INFI.API.Service;
using System.Collections.Generic;
using INFI.API.Models.AttachmentMngDto;
using Microsoft.AspNetCore.Authorization;

namespace INFI.API.Controllers
{
    [Authorize]
    [Route("infi/api/v1/[controller]")]
    public class AttachmentMngController : Controller
    {
        public IAttachmentMngService _attachmentMngService;
        public AttachmentMngController(IAttachmentMngService attachmentMngService)
        {
            _attachmentMngService = attachmentMngService;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AttachmentMngDto attachmentDto)
        {
            var result = await _attachmentMngService.AddOrDeleteAttachment(attachmentDto);
            return Ok(result);
        }
    }
}
