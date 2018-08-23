using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.PlanTaskMngDto
{
    public class ExcelUploadPlansPatams
    {
        public string InUserId { get; set; }
        public string SourceType { get; set; }    
        public List<ExcelPlans> Plans { get; set; }
        public List<ExcelTaskOfPlans> TaskOfPlans { get; set; }
        public List<ExcelTaskCard> TaskCard { get; set; }
        public List<ExcelTaskItem> TaskItem { get; set; }
        public List<ExcelCheckStandard> CheckStandard { get; set; }
        public List<ExcelScore> Score { get; set; }
        public List<ExcelCheckResult> CheckResult { get; set; }
        public List<ExcelPictureStandard> PictureStandard { get; set; }
    }
    public class ExcelPlans
    {
        public string Title { get; set; }
        public string DisCode { get; set; }
        public string VisitDateTime { get; set; }
        public string VisitType { get; set; }
        public string PId { get; set; }
    }
    public class ExcelTaskOfPlans
    {
        public string TPTitle { get; set; }
        public string TPDescription { get; set; }
        public string PId { get; set; }
        public string TCId { get; set; }
    }
    public class ExcelTaskCard
    {
        public string TCType { get; set; }
        public string TCRange { get; set; }
        public string TCTitle { get; set; }
        public string TCDescription { get; set; }
        public string DisCode { get; set; }
        public string TCId { get; set; }
        public string Kind { get; set; }
    }
    public class ExcelTaskItem
    {
        public string SeqNo { get; set; }
        public string Title { get; set; }
        public string ExeMode { get; set; }
        public string ScoreStandard { get; set; }
        public string TCId { get; set; }
        public string TIId { get; set; }
        public string ChapterType { get; set; }

    }
    public class ExcelCheckStandard
    {
        public string Ccontent { get; set; }
        public string TIId { get; set; }
    }
    public class ExcelScore
    {
        public string Score { get; set; }
        public string PlanApproalYN { get; set; }
        public string ResultApproalYN { get; set; }
        public string PassYN { get; set; }
        public string Remarks { get; set; }
        public string TPId { get; set; }
        public string TIId { get; set; }
        public string Type { get; set; }
        public string PlanFinishDate { get; set; }
        public string ResultFinishDate { get; set; }
    }
    public class ExcelCheckResult
    {
        public string Result { get; set; }
        public string TPId { get; set; }
        public string TIId { get; set; }
        public string CSId { get; set; }
    }
    public class ExcelPictureStandard
    {
        public string StandardPicName { get; set; }
        public string TIId { get; set; }
    }
}
