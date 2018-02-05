using System;
using System.Collections.Generic;

namespace INFI.API.Models.ImprovementDto
{
    public class ImpPlanDetailDto
    {
        public ImpPlanDetailDto()
        {
            StandardList = new List<StandardDto>();
            PicList = new List<PicDto>();
            AttachList = new List<AttachDto>();
            PicDescList = new List<PicDescDto>();
        }
        public string ImprovementCaption { get; set; }
        public string LostDescription { get; set; }
        public string ImprovementPlan { get; set; }
        public string ExpectedTime { get; set; }
        public string DisApprovalPlan { get; set; }
        public string FeedbackTime { get; set; }
        public string RegionApprovalPlan { get; set; }
        public string FeedbackRegionTime { get; set; }
        public string SPlanStatus { get; set; }
        public string SPlanStatusName { get; set; }
        public string RPlanStatus { get; set; }
        public string RPlanStatusName { get; set; }
        public string ExecDepartment { get; set; }
        public string ProcessId { get; set; }
        public string PlanApproval { get; set; }
        public bool AllocateYN { get; set; }
        public string PlanStatus { get; set; }
        public int Score { get; set; }
        public List<StandardDto> StandardList { get; set; }
        public List<PicDto> PicList { get; set; }
        public List<AttachDto> AttachList { get; set; }
        public List<PicDescDto> PicDescList { get; set; }
        public string PlanFinishDate { get; set; }
        public string ResultFinishDate { get; set; }
    }
    public class StandardDto
    {
        public int SeqNo { get; set; }
        public string CContent { get; set; }
        public string Result { get; set; }
    }
    public class PicDto
    {
        public int SeqNo { get; set; }
        public string PicName { get; set; }
        public string Url { get; set; }
        public string TypeName { get; set; }
    }
    public class AttachDto
    {
        public int SeqNo { get; set; }
        public string AttachName { get; set; }
        public string Url { get; set; }
    }
    public class PicDescDto
    {
        public int SeqNo { get; set; }
        public string PicDesc { get; set; }
        public string Url { get; set; }
    }
}
