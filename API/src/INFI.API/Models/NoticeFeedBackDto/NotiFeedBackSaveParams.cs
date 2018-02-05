using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.NoticeFeedBackDto
{
    public class NotiFeedBackSaveParams
    {
        public int NoticeId { get; set; }
        public int UserId { get; set; }
        public string ReplyContent { get; set; }
        public string Status { get; set; }
        public string XmlData { get; set; }
    }
}
