using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.HomeDto
{
    public class PushInfoDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? InDateTime { get; set; }
        public string InUserId { get; set; }
        public string DisId { get; set; }
        public DateTime? VisitDateTime { get; set; }
    }
}
