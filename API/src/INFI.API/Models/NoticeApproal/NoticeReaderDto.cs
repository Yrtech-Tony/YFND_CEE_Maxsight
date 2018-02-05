using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.NoticeApproal
{
    public class NoticeReaderDto
    {
        public int NoticeReaderId { get; set; }
        public string ReaderName { get; set; }
        public string FeedbackDateTime { get; set; }
        public string ReplyDateTime { get; set; }
        public string StatusCode { get; set; }
        public string StatusName { get; set; }
    }
}
