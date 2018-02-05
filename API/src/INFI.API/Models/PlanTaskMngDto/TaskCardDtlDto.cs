using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.PlanTaskMngDto
{
    public class TaskCardDtlDto
    {
        public string Id { get; set; }
        public string TCCode { get; set; }
        public string TCType { get; set; }
        public string TCRange { get; set; }
        public string TCTitle { get; set; }
        public string TCDescription { get; set; }
        public string IsUsed { get; set; }
        public string UseYN { get; set; }
        public string TCKind { get; set; }
        public List<TaskItemDto> TIList { get; set; }
        public List<CheckStandardDto> CSList { get; set; }
        public List<PictureStandardDto> PSList { get; set; }
    }

    public class TaskItemDto
    {
        public string Id { get; set; }
        public string SeqNo { get; set; }
        public string Title { get; set; }
        public string ExeMode { get; set; }
        public string ScoreStandard { get; set; }
        public string InUserId { get; set; }
        public string UserName { get; set; }
        public string InDateTime { get; set; }
        public string TCId { get; set; }
        public string TIId { get; set; }
    }

    public class CheckStandardDto
    {
        public string Id { get; set; }
        public string CContent { get; set; }
        public string TIId { get; set; }
        public string TCId { get; set; }
    }

    public class PictureStandardDto
    {
        public string Id { get; set; }
        public string StandardPicName { get; set; }
        public string TIId { get; set; }
        public string TCId { get; set; }
    }
}
