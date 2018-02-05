using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.NoticeFeedBackDto
{
    public class FeedBackListDto
    {
        public int NoticeId { get; set; }
        public string Status { get; set; }//状态
        public string Title { get; set; }
        public string NoticeNo { get; set; }
        public string StatusName { get; set; }
        public string NeedReply { get; set; }
        public string NeedReplyName { get; set; }
        public string InUserId { get; set; }
        public string MadeUserName { get; set; }
        public string MadeDate { get; set; }
        public string ReplyDate { get; set; }
        public string FeedbackDate { get; set; }
        public string FeedbackYN { get; set; }
        public DateTime InDateTime { get; set; }
        public string DepartId { get; set; }
        public string DepartName { get; set; }
        public string DisId { get; set; }
        public string DisName { get; set; }
    }
}
