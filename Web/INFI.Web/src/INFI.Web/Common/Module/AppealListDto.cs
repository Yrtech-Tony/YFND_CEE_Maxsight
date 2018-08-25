using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.Web.Common.Module
{
    public class AppealListDto
    {
        public string Id { get; set; }
        public string TIId { get; set; }
        public string Title { get; set; }
        public string TPId { get; set; }
        public string TPTitle { get; set; }
        public string DistributorId { get; set; }
        public string DisName { get; set; }
        public string DisCode { get; set; }
        public string ApprealDateTime { get; set; }
        public string AppealContent { get; set; }
        public string AppealResult { get; set; }
        public string ApprovalRemark { get; set; }
        public string ApprealUserId { get; set; }
        public string UserName { get; set; }
        public string SourceType { get; set; }
        public string SourceTypeName { get; set; }
        public string ScoreStandard { get; set; }
        public string SRemarks { get; set; }
        public string ApprovalDateTime { get; set; }

    }
}
