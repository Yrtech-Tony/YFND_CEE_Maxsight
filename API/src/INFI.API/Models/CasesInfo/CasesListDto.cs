using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.CasesInfo
{
    public class CasesListDto
    {
        public int Id { get; set; }
        public string CaseNo { get; set; }
        public string CaseType { get; set; }
        public string CaseTypeName { get; set; }
        public string CaseTitle { get; set; }
        public string CaseRegDate { get; set; }
		public string CaseRegUserName { get; set; }
    }
}
