using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.CalenderMngDto
{
    public class SaveCalenderMngParams
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public string SDate { get; set; }
        public string EDate { get; set; }
        public string UserID { get; set; }
    }
}
