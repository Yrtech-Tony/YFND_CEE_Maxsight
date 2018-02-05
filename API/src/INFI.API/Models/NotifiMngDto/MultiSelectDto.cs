using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.NotifiMngDto
{
    public class MultiSelectDto
    {
        public string DisCode { get; set; }
        public string DisName { get; set; }
        public bool IsChecked { get; set; }
        public int DisId { get; set; }
    }
}
