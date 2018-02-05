using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.HomeDto
{
    public class DoItemDto
    {
        public int SeqNo { get; set; }
        public string Title { get; set; }
		public string Status{get;set;}
		public string Id { get; set; }
		public string DataType { get; set; }
		public string TypeName { get; set; }
        public string Content { get; set; }
    }
}
