using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.Tour
{
    public class LocalDBUploadParams
    {
        public List<ScoreLDB> Score { get; set; }
        public List<CheckResultLDB> CheckResult { get; set; }
        public List<StandardPicLDB> StandardPic { get; set; }
        public List<TaskOfPlanLDB> TaskOfPlan { get; set; }
        public List<CustImproveItemDB> CustImproveItem { get; set; }


    }
    public class ScoreLDB
    {
        public int? TPId { get; set; }
        public int? ItemId { get; set; }
        public int? Scoreval { get; set; }
        public bool? PlanApproalYN { get; set; }
        public DateTime? PlanFinishDate { get; set; }
        public bool? ResultApproalYN { get; set; }
        public DateTime? ResultFinishDate { get; set; }
        public string PassYN { get; set; }
        public string Remarks { get; set; }
        public DateTime? InDateTime { get; set; }
        public int? InUserId { get; set; }
        public int? ModiUserId { get; set; }
        public DateTime? ModiDateTime { get; set; }
        public string Type { get; set; }

    }
    public class CheckResultLDB
    {
        public int? TPId { get; set; }
        public int? TIId { get; set; }
        public int? CSId { get; set; }
        public bool? Result { get; set; }
    }
    public class StandardPicLDB
    {
        public int? TPId { get; set; }
        public int? TIId { get; set; }
        public int? PSId { get; set; }
        public string Url { get; set; }
        public string PicName { get; set; }
        public string Type { get; set; }
        public string DelChk { get; set; }
        public string Id { get; set; }
    }
    public class TaskOfPlanLDB
    {
        public string TPCode { get; set; }
        public string TPTitle { get; set; }
        public string TPDescription { get; set; }
        public string Status { get; set; }
        public DateTime? SDateTime { get; set; }
        public DateTime? EDateTime { get; set; }
        public int? PId { get; set; }
        public int? TCId { get; set; }
        public int? InUserId { get; set; }
        public DateTime? InDateTime { get; set; }
        public int Id { get; set; }
    }

    public class CustImproveItemDB
    {
        public int PId { get; set; }
        public string TCTitle { get; set; }
        public string TCDescription { get; set; }
        public int UseYN { get; set; }
        public int InUserId { get; set; }
        public string SourceType { get; set; }
        public string TCKind { get; set; }
        public int SeqNo { get; set; }
        public string TiTitle { get; set; }
        public string ScoreStandard { get; set; }
        public string Status { get; set; }
        public string TPTitle { get; set; }
        public int Scoreval { get; set; }
        public bool PlanApproalYN { get; set; }
        public DateTime? PlanFinishDate { get; set; }
        public bool ResultApproalYN { get; set; }
        public DateTime? ResultFinishDate { get; set; }
        public string PassYN { get; set; }
        public string Remarks { get; set; }

    }
}
