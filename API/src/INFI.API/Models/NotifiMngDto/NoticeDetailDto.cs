using INFI.API.Models.ImprovementDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.NotifiMngDto
{
    public class NoticeDetailDto
    {
        public NoticeDetailDto()
        {
            AttachList = new List<AttachDto>();
            NoticeDisList = new List<MultiSelectDto>();
            NoticeDepList = new List<MultiSelectDto>();
        }
        public string Title { get; set; }//通知标题
        public string NoticeNo { get; set; }
        public DateTime SDate { get; set; }//通知有效期开始日期
        public DateTime EDate { get; set; }//通知有效期结束日期
        public int NeedReply { get; set; }//是否反馈
        public string NeedReplyName { get; set; }//是否反馈
        public string Content { get; set; }//通知内容
        public string Status { get; set; }//状态
        public int NoticeId { get; set; }
        public List<AttachDto> AttachList { get; set; }
        public List<MultiSelectDto> NoticeDisList { get; set; }
        public List<MultiSelectDto> NoticeDepList { get; set; }
    }
}
