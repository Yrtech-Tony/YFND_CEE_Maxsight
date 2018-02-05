using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.ImprovementDto
{
    public class ImpAllocateDto
    {
        public ImpAllocateDto()
        {
            StandardList = new List<StandardDto>();
            PicList = new List<PicDto>();
            PicDescList = new List<PicDescDto>();
        }
        public string PlanStatus { get; set; }
        public string ExecDepartmentName { get; set; }
        public bool AllocateYN { get; set; }
        public bool PlanApproalYN { get; set; }
        public bool ResultApproalYN { get; set; }
        public string ImprovementCaption { get; set; }
        public string LostDescription { get; set; }
        public int Score { get; set; }
        public string PlanFinishDate { get; set; }
        public string ResultFinishDate { get; set; }
        public List<StandardDto> StandardList { get; set; }
        public List<PicDto> PicList { get; set; }
        public List<PicDescDto> PicDescList { get; set; }
    }
}
