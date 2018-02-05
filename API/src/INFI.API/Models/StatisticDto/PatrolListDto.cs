using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.StatisticDto
{
    public class PatrolListDto
    {
        public string Id { get; set; }//计划标题ID
        public string Title { get; set; }//计划任务标题
        public string Name { get; set; }//经销商
        public string VisitDateTime { get; set; }//拜访时间
        public string VisitType { get; set; }//拜访类型
        public string VisitTypeName { get; set; }//拜访名
        public string PStatus { get; set; }//状态
        public string PStatusName { get; set; }//状态名
        public string Totle { get; set; }//计划下的任务总数
        public string Complete { get; set; }//完成的任务数
        public decimal Rate { get; set; }//完成率
        public string TCType { get; set; }
        public string InDateTime { get; set; }//制作时间
        public string InUserId { get; set; }//制作人ID
        public string UserName { get; set; }//制作人姓名
        public string DisId { get; set; }//所属区域Id
        public string DisName { get; set; }//所属区域名
        public string SourceCode { get; set; }//来源Code
        public string SourceName { get; set; }//来源名
        public string LastDate { get; set; }//最后一次更新时间
    }
}
