using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.Tour
{
    public class ItemInfoForScore
    {
        public int TIId { get; set; }
        public int SeqNo { get; set; }
        public string Title { get; set; }
        public string ExeMode { get; set; }
        public string ScoreStandard { get; set; }
        public int MaxSeqNo { get; set; }
        public int TPId { get; set; }
        public List<CheckStandard> CSList { get; set; }
        public List<StandardPic> SPicList { get; set; }


    }
}
