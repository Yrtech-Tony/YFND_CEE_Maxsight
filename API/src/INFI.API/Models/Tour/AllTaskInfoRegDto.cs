using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.Tour
{
    public class AllTaskInfoRegDto
    {
        public string TPType { get; set; }
        public CustomizedTaskDto CustomizedTask { get; set; }
        public ScoreCheckResultParam ScoreCheckResult { get; set; }
    }
    public class AllTaskInfoRegLstDto
    {
        public List<AllTaskInfoRegDto> TaskInfoRegDtoLst { get; set; }
    }
}
