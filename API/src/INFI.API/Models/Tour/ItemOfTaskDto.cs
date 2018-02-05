using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.Tour
{
    public class ItemOfTaskDto
    {
        public int TPId { get; set; }
        public int TIId { get; set; }
        public int SeqNo { get; set; }
        public string Title { get; set; }
        public string PassYN { get; set; }
        public int Score { get; set; }
        public bool PlanApproalYN { get; set; }
        public bool ResultApproalYN { get; set; }
        public string ExeMode { get; set; }
        public string ScoreStandard { get; set; }
        public bool IsClicked { get; set; }
        public string Remarks { get; set; }
        public int APId { get; set; }
        public string AppealYN { get; set; }

        public string AppealResult { get; set; }

        public DateTime? PlanFinishDate { get; set; }
        public DateTime? ResultFinishDate { get; set; }
        public ObservableCollection<CheckStandard> CSList { get; set; }
        public ObservableCollection<StandardPic> SPicList { get; set; }
        public ObservableCollection<PictureStandard> PStandardList { get; set; }
    }
    public class CheckStandard
    {
        public int TPId { get; set; }
        public int TIId { get; set; }
        public int SeqNo { get; set; }
        public int CSID { get; set; }
        public string CContent { get; set; }
        public bool IsCheck { get; set; }
    }
    public class StandardPic
    {
        public int TPId { get; set; }
        public int TIId { get; set; }
        public int SeqNo { get; set; }
        public string PicName { get; set; }
        public string Url { get; set; }
        public string PicType { get; set; }
        public int PicId { get; set; }
    }
    public class PictureStandard
    {
        public int TPId { get; set; }
        public int TIId { get; set; }
        public int SeqNo { get; set; }
        public string StandardPicName { get; set; }
        public int StandardPicId { get; set; }
        public string Url { get; set; }
        public string SuccessImage { get; set; }
        public string GRUD { get; set; }
    }
}
