using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.Web.Common.Module
{
    public class AttachmentMngDto
    {
        public int Id { get; set; }
        public int RefId { get; set; }
        public string Type { get; set; }
        public string AttachName { get; set; }
        public string Url { get; set; }
    }
}
