using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.NoticeApproal
{
    public class Distributor
    {
        public int Id { get; set; }
        public string DisCode { get; set; }
        public string DisName { get; set; }

    }
    public class ApprovalStatus
    {
        public object Code;
        public string Name;
    }
}
