using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.Web.Common.Module
{
    public class RecordInfoDto
    {
        public string RecordType { get; set; }
        public string RecordTitle { get; set; }
        public string ApprovalStatus { get; set; }
        public string RecordDatetime { get; set; }
        public string RecordUserName { get; set; }
        public string ApprovalUserName { get; set; }
        public string RecordReason { get; set; }
        public string RecordUserId { get; set; }
        public string ApprovalRemark { get; set; }
        public List<AttachmentMngDto> AttachmentList { get; set; }
        public List<AttachmentMngDto> ApproalAttachList { get; set; }
    }
}
