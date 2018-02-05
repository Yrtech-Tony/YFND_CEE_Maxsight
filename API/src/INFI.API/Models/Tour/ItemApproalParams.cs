using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.Tour
{
    public class ItemApproalParams
    {
        public int TPId { get; set; }
        public int TIId { get; set; }
        public int Score { get; set; }
        public bool? PlanApproalYN { get; set; }
        public bool? ResultApproalYN { get; set; }
        public int UserId { get; set; }
    }
}
