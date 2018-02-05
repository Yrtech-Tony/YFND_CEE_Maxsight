using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.ReportDto
{
    public class ReportAttachmentDto
    {
        public int Id { get; set; }
        public string AttachName { get; set; }
        public string Code { get; set; }
        public string SourceType { get; set; }
        public string DisName { get; set; }
        public string DownloadCnt { get; set; }
        public string Url { get; set; }
        public string InDateTime { get; set; }
        public string DisCode { get; set; }
        public string GRUD { get; set; }
    }
}
