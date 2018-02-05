using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.Tour
{
    public class TaskOfPlanDto
    {
        public int TPId { get; set; }
        public string TCCode { get; set; }
        public string TCTitle { get; set; }
        public string TPStatus { get; set; }
        public string TPType { get; set; }
        public string SourceType { get; set; }
        public string PTitle { get; set; }
        public string UserName { get; set; }
    }
}
