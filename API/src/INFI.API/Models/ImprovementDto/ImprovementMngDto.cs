using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.ImprovementDto
{
    public class ImprovementMngDto
    {
        public string PlanTitle { get; set; }
		public string TaskCardTitle { get; set; }
		public string TaskItemTitle { get; set; }
		//public string CheckStandardList { get; set; }
		public bool PlanApproalYN { get; set; }
        public bool ResultApproalYN { get; set; }
		public int ItemId { get; set; }
        public int TPId { get; set; }
		public int Score {get;set;}
        public string PlanStatus { get; set; }
		public string ResultStatus { get; set; }
        public string PlanStatusName { get; set; }
        public string ResultStatusName { get; set; }
        public int ExecDepartment { get; set; }
        public string ExecDepartmentName { get; set; }
        public DateTime FeedbackTime { get; set; }
        public DateTime FeedbackRegionTime { get; set; }
		public int ImprovementId { get; set; }
        public int ImpResultId { get; set; }
        public string DoScoreDate { get; set; }
        public string DoAllocateDate { get; set; }
        public string SourceType { get; set; }
	    public string SourceTypeName { get; set; }
        public bool AllocateYN { get; set; }
    }
}
