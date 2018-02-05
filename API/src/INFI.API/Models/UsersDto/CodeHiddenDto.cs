using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.UsersDto
{
    public class CodeHiddenDto
    {
        public string RowId { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string UserYN { get; set; }
    }
}
