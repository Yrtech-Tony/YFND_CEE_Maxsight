using System.Collections.Generic;

namespace INFI.API.Models.ImprovementDto
{
    public class ImprovementResultSaveParamDto
    {
        public string ImprovementId { get; set; }
        public string ImpResultId { get; set; }
        public string ResultStatus { get; set; }
        public string ResultContent { get; set; }
        public string InUserId { get; set; }
        public List<AttachDto> AttachList { get; set; }
    }
}
