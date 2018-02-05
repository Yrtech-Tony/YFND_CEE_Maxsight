using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.PlanTaskMngDto
{
    public class PlanDtlDto
    {
        public string SeqNo { get; set; }
        public string Id { get; set; }//计划任务ID
        public string DistributorId { get; set; }//经销商ID
        public string Name { get; set; }//经销商姓名
        public string Code { get; set; }//经销商Code
        public string  VisitDateTime { get; set; }//拜访时间
        public string VisitType { get; set; }//拜访类型
        public string VisitTypeName { get; set; }
        public string Title { get; set; }//计划任务标题
        public string PStatus { get; set; }//状态
        public string PStatusName { get; set; }//状态名
        public string RejectReason { get; set; }//理由
        public string TPCode { get; set; }//任务Code
        public string TPTitle { get; set; }//任务标题
        public string TPDescription { get; set; }//任务概述
        public string TPId { get; set; }//任务ID
        public string InUserId { get; set; }//登记人ID
        public string UserName { get; set; }//登记人姓名
        public string InDateTime { get; set; }
        public string TCId { get; set; }
        public string TCType { get; set; }
    }
}
