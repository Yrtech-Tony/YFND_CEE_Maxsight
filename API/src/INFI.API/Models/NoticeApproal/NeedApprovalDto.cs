using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.NoticeApproal
{
    public class NeedApprovalDto
    {
        public int NoticeReaderId { get; set; }
        public string NoticeNo { get; set; }
        public string Title { get; set; }
        public string DisCode { get; set; }
        public string DisName { get; set; }
        public string DepartName { get; set; }
        public string Status { get; set; }
        public string ApproalName { get; set; }
        public int InUserId { get; set; }
    }
    public class NeedApprovalDto2
    {
        public int NoticeReaderId { get; set; }
        public string NoticeNo { get; set; }
        public string Title { get; set; }
        public string ReaderName { get; set; }
        public string FeedBackDate { get; set; }
    }

    public class NeedApprovalListDto
    {
        public string NoticeNo { get; set; }
        public string Title { get; set; }
        public string InDateTime { get; set; }
        public string UserName { get; set; }
        public int NoticeId { get; set; }
    }
}
