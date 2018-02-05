using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.UsersDto
{
    public class UpdateTypeParam
    {
        public string InUserId { get; set; }
        public List<CodeHiddenDto> list { get; set; }
    }
}
