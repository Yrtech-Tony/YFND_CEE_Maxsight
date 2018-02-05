using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.ImprovementDto
{
    public class ImprovementParamDto
    {
        public int tPId { get; set; }
        public int itemId { get; set; }
        public int departmentId { get; set; }
        public string allocateYN { get; set; }
        public string improvementCaption { get; set; }
        public string lostDescription { get; set; }
        public int inUserId { get; set; }
    }
}
