using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.NoticeApproal
{
    public class ApproalLogDto
    {
        public string NoticeNo { get; set; }
        public string Title { get; set; }
        public string ApprovalStatus { get; set; }
        public string ReplyDateTime { get; set; }

        public string ReplyContent { get; set; }
        public string FeedbackContent { get; set; }
        public string PassYNContent { get; set; }
        public bool PassYN { get; set; }
        public string FeedbackDateTime { get; set; }

    }
}
