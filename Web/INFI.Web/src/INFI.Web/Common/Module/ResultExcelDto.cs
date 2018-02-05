using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.Web.Common.Module
{
    public class ResultExcelDto
    {
        public string PId { get; set; }
        public string PTitle { get; set; }
        public string TPId { get; set; }
        public string TCId { get; set; }
        public string TCCode { get; set; }
        public string TCTitle { get; set; }
        public string TIId { get; set; }
        public string TITitle { get; set; }
        public string CSId { get; set; }
        public string CSTitle { get; set; }
        public string CRId { get; set; }
        public string Result { get; set; }
        public string SId { get; set; }
        public string Score { get; set; }
        public string Remarks { get; set; }
        public string TPStatus { get; set; }
        public string TPType { get; set; }
        public string SourceType { get; set; }
        public string PassYN { get; set; }
        public string DisCode { get; set; }
    }
    public class LosePic
    {
        public string PicUrl { get; set; }
        public string PicName { get; set; }
    }
    public class ExcelResult
    {
        public List<ResultExcelDto> ResultList { get; set; }
        public List<LosePic> LPicList { get; set; }
    }
}
