using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.RecordDto
{
    public class UpdateRecordInfoParam
    {
        public string UserId { get; set; }
        public string Id { get; set; }
        public string ApprovalStatus { get; set; }
        public string ApprovalRemark { get; set; }
        public List<AttachmentMngDto.AttachmentMngDto> ApproalAttachList { get; set; }
    }
}
