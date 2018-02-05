using System.Collections.Generic;

namespace INFI.API.Models.AppealMngDto
{
    public class UpdateAppealInfoParam
    {
        public string UserId { get; set; }
        public string Id { get; set; }
        public string AppealResult { get; set; }
        public string ApprovalRemark { get; set; }
        public List<AttachmentMngDto.AttachmentMngDto> ApproalAttachList { get; set; }
    }
}
