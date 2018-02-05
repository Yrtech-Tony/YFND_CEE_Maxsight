using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.StatisticDto
{
    public class InfoByDisIdDto
    {
        public string AreaId { get; set; }
        public string DisId { get; set; }
        public string AreaName { get; set; }
        public string DisName { get; set; }
        public string DUserName { get; set; }
        public string AUserName { get; set; }
        public string NowVisitDate { get; set; }
        public string LastDate { get; set; }
    }
}
