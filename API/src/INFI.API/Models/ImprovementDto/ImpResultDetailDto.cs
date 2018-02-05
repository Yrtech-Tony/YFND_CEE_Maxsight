using System.Collections.Generic;

namespace INFI.API.Models.ImprovementDto
{
    public class ImpResultDetailDto
    {
        public ImpResultDetailDto()
        {
            AttachList = new List<AttachDto>();
        }
        public string ReplyContent { get; set; }
        public string ApprovalSContent { get; set; }
        public string SResultStatus { get; set; }
        public string SResultStatusName { get; set; }
        public string ApprovalZContent { get; set; }
        public string ZResultStatus { get; set; }
        public string ZResultStatusName { get; set; }
        public string ResultStatus { get; set; }
        public string SApprovalDate { get; set; }
        public string ZApprovalDate { get; set; }
        public List<AttachDto> AttachList { get; set; }
    }
}
