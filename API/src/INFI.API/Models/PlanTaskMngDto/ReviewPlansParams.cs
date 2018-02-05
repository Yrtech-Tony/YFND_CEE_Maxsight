using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.PlanTaskMngDto
{
    public class ReviewPlansParams
    {
        public string Id { get; set; }
        public string PStatus { get; set; }
        public string RejectReason { get; set; }

        public string UserId { get; set; }


    }
}
