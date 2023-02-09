using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPathApp.PersistanceLayer.DTOs
{
    public class ChartPoint
    {
        public double Y { get; set; }
        public int X { get; set; }

    }

    public class ChartDataDTO
    {
        public IEnumerable<ChartPoint> Data { get; set; }
        public double YMax { get; set; }
        public double YMin { get; set; }

        public int XMin = 1;

        public int XMax = DateTime.Now.Day;
        public int TickAmount { get; set; }

        public double ProgressPercentage { get; set; }
    }
}
