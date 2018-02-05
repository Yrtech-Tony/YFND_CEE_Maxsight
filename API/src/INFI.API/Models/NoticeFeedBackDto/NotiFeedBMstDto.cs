using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models
{
    public class NotiFeedBMstDto
    {
        public string NoticeNo { get; set; }//通知编号

        public string Title { get; set; }//通知标题
        public string NeedReply { get; set; }//是否反馈Code
        public string ReplyName { get; set; }//是否反馈
        public string ApproveCode { get; set; }//审批状态Code
        public string ApproveName { get; set; }//审批状态名
        public string NoticeRoles { get; set; }//通知角色
        public string InDate { get; set; }//制作时间
        public string UserName { get; set; }//制作人
        public string NoticeStatus { get; set; }//通知状态Code
        public string NoticeStatusName { get; set; }//通知状态名
        public int NoticeReaderId { get; set; } //NoticeReaderId
    }
}
