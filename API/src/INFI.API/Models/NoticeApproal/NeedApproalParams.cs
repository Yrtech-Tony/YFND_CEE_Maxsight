using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.NoticeApproal
{
    public class NeedApproalParams
    {
        public string NoticeReaderId { get; set; }
        public bool PassYN { get; set; }
        public string ReplyContent { get; set; }
        public int UserId { get; set; }
    }
}
