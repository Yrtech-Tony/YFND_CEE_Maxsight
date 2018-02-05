using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models
{
    public class NotiFeedBDtlDto
    {
        public int NoticeId { get; set; }
        public string NoticeNo { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public string ReplyContent { get; set; }
        public string FeedbackContent { get; set; }
        public string PassYN { get; set; }   
    } 
}
