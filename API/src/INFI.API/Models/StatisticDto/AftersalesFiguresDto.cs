using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.StatisticDto
{
    public class AftersalesFiguresDto
    {
        public AftersalesFiguresDto()
        {
            BusinessList = new List<BusinessDto>();
            QualityList = new List<QualityDto>();
            PartsStockList = new List<PartsStockDto>();
            CSSYearTargetList = new List<StatisticDto.CSSYearTargetDto>();
        }
        public List<BusinessDto> BusinessList { get; set; }
        public List<QualityDto> QualityList { get; set; }
        public List<PartsStockDto> PartsStockList { get; set; }
        public List<CSSYearTargetDto> CSSYearTargetList { get; set; }
    }
    public class BusinessDto
    {
        public string LTypeCode { get; set; }
        public string LTypeName { get; set; }
        public string YearMonth { get; set; }
        public string Actual { get; set; }
        public string Target { get; set; }
        public string Rate { get; set; }
        public string ActualYTM { get; set; }
        public string TargetYTM { get; set; }
        public string RateYTM { get; set; }

    }
    public class QualityDto
    {
        public string LTypeCode { get; set; }
        public string LTypeName { get; set; }
        public string YearMonth { get; set; }
        public string Score { get; set; }
        public string Rank { get; set; }
        public string Average { get; set; }
        public string Total { get; set; }
        public string ScoreAndRank { get; set; }
        public string AverageAndTotal { get; set; }
    }
    public class PartsStockDto
    {
        public string LTypeCode { get; set; }
        public string LTypeName { get; set; }
        public string YearMonth { get; set; }
        public string Target { get; set; }
        public string Actual { get; set; }
    }
    public class CSSYearTargetDto
    {
        public string CSSYearTarget { get; set; }
    }
}
