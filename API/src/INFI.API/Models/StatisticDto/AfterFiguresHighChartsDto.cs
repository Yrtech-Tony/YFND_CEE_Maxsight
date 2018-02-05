using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Models.StatisticDto
{
    public class AfterFiguresHighChartsDto
    {
        public AfterFiguresHighChartsDto()
        {
            Throughputs = new List<HighChartsDto>();
            Parts = new List<HighChartsDto>();
            AfterSales = new List<HighChartsDto>();
            PartsStock = new List<HighChartsDto>();
            CSS = new List<HighChartsDto>();
            FFV = new List<HighChartsDto>();
            IKB = new List<HighChartsDto>();         
        }
        public List<HighChartsDto> Throughputs { get; set; }
        public List<HighChartsDto> Parts { get; set; }

        public List<HighChartsDto> AfterSales { get; set; }

        public List<HighChartsDto> PartsStock { get; set; }

        public List<HighChartsDto> CSS { get; set; }

        public List<HighChartsDto> FFV { get; set; }

        public List<HighChartsDto> IKB { get; set; }

    }

    public class HighChartsDto
    {
        public string LTypeCode { get; set; }
        public string LTypeName { get; set; }
        public string Stype { get; set; }
        public string STypeName { get; set; }
        public decimal? Jan { get; set; }
        public decimal? Feb { get; set; }

        public decimal? Mar { get; set; }

        public decimal? Apr { get; set; }

        public decimal? May { get; set; }

        public decimal? Jun { get; set; }

        public decimal? July { get; set; }

        public decimal? Aug { get; set; }

        public decimal? Sep { get; set; }

        public decimal? Oct { get; set; }

        public decimal? Nov { get; set; }

        public decimal? Dec { get; set; }

    }
}
