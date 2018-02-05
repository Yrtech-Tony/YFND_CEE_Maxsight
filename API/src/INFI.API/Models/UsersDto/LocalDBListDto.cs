using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.UsersDto
{
    public class LocalDBListDto
    {
        public List<PlansDto> PlansLst { get; set; }
        public List<DistributorDto> DistributorLst { get; set; }
        public List<EmployeeDto> EmployeeLst { get; set; }
        public List<TaskOfPlanDto> TaskOfPlanLst { get; set; }
        public List<TaskCardDto> TaskCardLst { get; set; }
        public List<TaskItemDto> TaskItemLst { get; set; }
        public List<CheckStandardDto> CheckStandardLst { get; set; }
        public List<PictureStandardDto> PictureStandardLst { get; set; }
        public List<ScoreDto> ScoreLst { get; set; }
        public List<StandardPicDto> StandardPicLst { get; set; }
        public List<CheckResultDto> CheckResultLst { get; set; }
        public List<HiddenCodeDto> HiddenCodeLst { get; set; }
    }
    public class PlansDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? DistributorId { get; set; }
        public DateTime? VisitDateTime { get; set; }
        public string VisitType { get; set; }
        public string PStatus { get; set; }
        public string RejectReason { get; set; }
        public int? Batch { get; set; }
        public int? InUserId { get; set; }
        public DateTime? InDateTime { get; set; }
        public int? ModiUserId { get; set; }
        public DateTime? ModiDateTime { get; set; }
    }
    public class DistributorDto
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool? UseYN { get; set; }
        public int? InUserid { get; set; }
        public DateTime? InDateTime { get; set; }
    }
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string UserType { get; set; }
        public int? DisId { get; set; }
        public int? DepartId { get; set; }
        public string UserName { get; set; }
        public string CellNo { get; set; }
        public int? UserId { get; set; }
        public bool? UserYN { get; set; }
    }
    public class TaskOfPlanDto
    {
        public int Id { get; set; }
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
    }
    public class TaskCardDto
    {
        public int Id { get; set; }
        public string TCCode { get; set; }
        public string TCType { get; set; }
        public string TCRange { get; set; }
        public string TCTitle { get; set; }
        public string TCDescription { get; set; }
        public int? DisId { get; set; }
        public int? UseYN { get; set; }
        public int? InUserId { get; set; }
        public DateTime? InDateTime { get; set; }
        public int? ModiUserId { get; set; }
        public DateTime? ModiDateTime { get; set; }
        public string SourceType { get; set; }
    }
    public class TaskItemDto
    {
        public int Id { get; set; }
        public int? SeqNo { get; set; }
        public string Title { get; set; }
        public string ExeMode { get; set; }
        public int? Tcid { get; set; }
        public string ScoreStandard { get; set; }
        public int? InUserId { get; set; }
        public DateTime? InDateTime { get; set; }
        public int? ModiUserId { get; set; }
        public DateTime? ModiDateTime { get; set; }
    }
    public class CheckStandardDto
    {
        public int Id { get; set; }
        public string CContent { get; set; }
        public int TIId { get; set; }
        public int InUserId { get; set; }
        public DateTime? InDateTime { get; set; }
        public int? ModiUserId { get; set; }
        public DateTime? ModiDateTime { get; set; }
    }
    public class PictureStandardDto
    {
        public int Id { get; set; }
        public int? TIId { get; set; }
        public string StandardPicName { get; set; }
        public string Url { get; set; }
        public int? InUserId { get; set; }
        public DateTime? InDateTime { get; set; }
        public int? ModiUserId { get; set; }
        public DateTime? ModiDateTime { get; set; }
    }
    public class ScoreDto
    {
        public int Id { get; set; }
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

    }
    public class StandardPicDto
    {
        public int Id { get; set; }
        public int? TPId { get; set; }
        public int? TIId { get; set; }
        public int? PSId { get; set; }
        public string Url { get; set; }
        public string PicName { get; set; }
        public string Type { get; set; }
    }
    public class CheckResultDto
    {
        public int Id { get; set; }
        public int? TPId { get; set; }
        public int? TIId { get; set; }
        public int? CSId { get; set; }
        public bool? Result { get; set; }
    }
    public class HiddenCodeDto
    {
        public int Id { get; set; }
        public string GroupCode { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public int DisSeq { get; set; }
        public bool? UseYN { get; set; }
    }
}
