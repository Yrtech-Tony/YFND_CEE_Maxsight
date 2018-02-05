using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.Tour
{
    public class CustomizedTaskDto
    {
        public int TPId { get; set; }
        public int TCId { get; set; }
        public int ScoreId { get; set; }
        public string Remarks { get; set; }
        public int UserId { get; set; }
    }
}
