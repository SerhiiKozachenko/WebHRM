using System.Collections.Generic;

namespace Hrm.Web.Models.Selection
{
    public class ChartSeriesModel
    {
        public string Name { get; set; }

        public double CategoryAvgEstimate { get; set; }

        public IList<string> Categories { get; set; }

        public IList<double> Data { get; set; }

        public int YAxis { get; set; }

        public ChartSeriesModel()
        {
            this.Categories = new List<string>();
            this.Data = new List<double>();
        }
    }
}