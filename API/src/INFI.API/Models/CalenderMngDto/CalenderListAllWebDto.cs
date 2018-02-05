using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.CalenderMngDto
{
    public class CalenderListAllWebDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string SDate { get; set; }
        public string EDate { get; set; }
        public string Type { get; set; }
        public string TypeName { get; set; }
        public string UserId { get; set; }
        public string DisId { get; set; }
        public string DepartId { get; set; }
    }
    public class EachDayTypeDto
    {
        public string EachDate { get; set; }
        public string Type { get; set; }
    }
}
