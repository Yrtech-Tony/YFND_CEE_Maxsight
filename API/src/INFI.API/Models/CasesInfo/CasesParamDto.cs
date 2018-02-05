using INFI.API.Models.ImprovementDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.CasesInfo
{
    public class CasesParamDto
    {
        public int Id { get; set; }
        public string CaseType { get; set; }
        public string CasePoint { get; set; }
        public string LossRemark { get; set; }
        public string ImproveRemark { get; set; }
        public int InUserId { get; set; }
        public string CaseTitle { get; set; }
        public  List<AttachDto> AttachList { get; set; }
    }
    public class CasesDelParamDto
    {
        public int InUserId { get; set; }
        public List<IdParamDto> IdList { get; set; }
    }
    public class IdParamDto
    {
        public int Id { get; set; }
    }
}
