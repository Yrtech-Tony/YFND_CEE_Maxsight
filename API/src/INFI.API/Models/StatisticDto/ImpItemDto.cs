using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.StatisticDto
{
    public class ImpItemDto
    {
        public string AreaName { get; set; }
		public string DisName { get; set; }
		public string TaskItemTitle { get; set; }
		public string ExecDepartmentName { get; set; }
		public string ImpStatusName { get; set; }
		public string LastUpdateTime { get; set; }
        public int DisId { get; set; }
        public string ImpStatus { get; set; }
    }
}
