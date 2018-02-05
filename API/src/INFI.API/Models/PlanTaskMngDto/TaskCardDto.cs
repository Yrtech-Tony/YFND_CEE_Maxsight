using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.PlanTaskMngDto
{
    public class TaskCardDto
    {
        public string Id { get; set; }
        public string TCCode { get; set; }
        public string TCType { get; set; }
        public string TCRange { get; set; }
        public string TCTitle { get; set; }
        public string TCDescription { get; set; }
        public string InUserId { get; set; }
        public string TypeName { get; set; }
        public string RangeName { get; set; }
        public string UserName { get; set; }
        public string InDateTime { get; set; }
        public string DisId { get; set; }
        public string IsEdit { get; set; }
        public string UseYN { get; set; }
        public string SourceCode { get; set; }
        public string SourceName { get; set; }
        public string KindCode { get; set; }
        public string KindName { get; set; }
    }
}
