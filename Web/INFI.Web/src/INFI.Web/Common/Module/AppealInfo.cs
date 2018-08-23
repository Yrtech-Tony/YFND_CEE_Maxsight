using System;
using System.Collections.Generic;

namespace INFI.Web.Common.Module
{

    public class AppealInfo
    {
        public int Id { get; set; }
        public int TPId { get; set; }
        public int TIId { get; set; }
        public string AppealContent { get; set; }
        public string AppealResult { get; set; }
        public string ApprovalRemark { get; set; }
        public int ApprealUserId { get; set; }
        public DateTime? ApprealDateTime { get; set; }
        public int ApprovalUserId { get; set; }
        public DateTime? ApprovalDateTime { get; set; }
        public string SRemarks { get; set; }
        public List<AttachmentMngDto> AttachmentList { get; set; }
        public List<AttachmentMngDto> ApproalAttachList { get; set; }

    }


}
