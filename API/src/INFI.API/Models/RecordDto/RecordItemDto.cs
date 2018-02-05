using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.RecordDto
{
    public class RecordItemDto
    {
        public int DisId { get; set; }
        public string RecordType { get; set; }
        public string RecordTitle { get; set; }
        public string ApproalStatus { get; set; }
        public string RecordDatetime { get; set; }
        public string RecordUserName { get; set; }
        public string ApprovalUserName { get; set; }
        public string RecordReason { get; set; }
        public string RecordUserId { get; set; }
        public string DisName { get; set; }
        public int RId { get; set; }
        public string ApproalStatusCode { get; set; }
        public string ApprovalDateTime { get; set; }
        public string ApprovalRemark { get; set; }
    }
}
