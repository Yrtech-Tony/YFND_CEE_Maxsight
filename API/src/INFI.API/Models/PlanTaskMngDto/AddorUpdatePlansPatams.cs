using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.PlanTaskMngDto
{
    public class AddorUpdatePlansPatams
    {
        public string PId { get; set; }
        public string Title { get; set; }
        public string DistributorId { get; set; }
        public string VisitDateTime { get; set; }
        public string VisitType { get; set; }
        public string PStatus { get; set; }
        public string InUserId { get; set; }
        //public string XmlData { get; set; }
        public List<TCardDto> XmlData { get; set; }  
        public string list { get; set; }
    }
}
