using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.Tour
{
    public class CustomizedImpItemDto
    {
        public int PId { get; set; }
        public string ImpTitle { get; set; }
        public string ImpDesc { get; set; }
        public bool PlanApproalYN { get; set; }
        public DateTime PlanFinishDate { get; set; }
        public bool ResultApproalYN { get; set; }
        public DateTime ResultFinishDate { get; set; }
        public string Remark { get; set; }
        public int InUserId { get; set; }
        public string TCKind { get; set; }
    }
}
