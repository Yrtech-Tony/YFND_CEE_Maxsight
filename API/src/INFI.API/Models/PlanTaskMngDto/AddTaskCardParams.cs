using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.PlanTaskMngDto
{
    public class AddTaskCardParams
    {
        public string Id { get; set; }
        public string TCType { get; set; }
        public string TCRange { get; set; }
        public string TCTitle { get; set; }
        public string SourceType { get; set; }
        public string TCDescription { get; set; }
        public string InUserId { get; set; }
        public string UseYN { get; set; }
        public string Kind { get; set; }
        public List<AddTaskItemsDto> TaskItem { get; set; }
        public List<AddCheckStandardDto> XmlDataS { get; set; }
        public List<AddStandardPicDto> XmlDataP { get; set; }
        public List<TaskItemDto> DelTIList { get; set; }
        public List<CheckStandardDto> DelCSList { get; set; }
        public List<PictureStandardDto> DelPSList { get; set; }
    }

    public class AddCheckStandardDto
    {
        public string CContent { get; set; }
        public string TIRowId { get; set; }
        public string Id { get; set; }
        public string TIId { get; set; }
    }

    public class AddStandardPicDto
    {
        public string StandardPicName { get; set; }
        public string TIRowId { get; set; }
        public string Id { get; set; }
        public string TIId { get; set; }
    }

    public class AddTaskItemsDto
    {
        public string SeqNo { get; set; }
        public string Title { get; set; }
        public string ExeMode { get; set; }
        public string ScoreStandard { get; set; }
        public string TIRowId { get; set; }

        public string Id { get; set; }

    }
}
