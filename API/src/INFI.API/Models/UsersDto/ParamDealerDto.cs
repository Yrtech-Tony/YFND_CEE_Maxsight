using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.UsersDto
{
    public class ParamDealerDto
    {
        public ParamDealerDto()
        {
            DisList = new List<DealerDto>();
        }
        public int InUserId { get; set; }
        public List<DealerDto> DisList { get; set; }
    }
    public class DealerDto
    {
        public string AreaCode { get; set; }
        public string DisCode { get; set; }
        public string DisName { get; set; }
    }
}
