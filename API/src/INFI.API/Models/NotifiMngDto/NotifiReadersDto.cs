using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.NotifiMngDto
{
    public class NotifiReadersDto
    {
        public int NoticeId { get; set; }
        public string Status {get;set;}
        public int ReaderId { get; set; }
        public string StatusName { get; set; }
        public string ReplyDate { get; set; }
        public string FeedbackDate { get; set; }
        public string SeqNo { get; set; }
        public string NoticeReaderName { get; set; }
        public string FeedbackYN { get; set; }
        public int DisId { get; set; }
        public int DepartId { get; set; }
    }
}
