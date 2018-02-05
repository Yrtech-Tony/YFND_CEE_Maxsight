using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.ImprovementDto
{
    public class ImpListDto
    {
       public int ItemId { get; set; }
	   public string TCKindName { get; set; }
	   public string ItemName { get; set; }
	   public string ScoreRemark { get; set; }
	   public string ScoreDate { get; set; }
	   public bool PlanApproalYN { get; set; }                           
	   public bool ResultApproalYN { get; set; }                                 
	   public string PlanFinishDate { get; set; }
	   public string ResultFinishDate { get; set; }
	   public string StatusName { get; set; }
	   public string StatusCode { get; set; }
	   public string StatusType { get; set; }
	   public string SourceTypeName { get; set; }
	   public int TPId { get; set; }
	   public int ImpId { get; set; }                                
	   public int ImpResultId { get; set; }
       public bool AllocateYN { get; set; }
       public string ExecDepartmentName { get; set; }
    }
}
