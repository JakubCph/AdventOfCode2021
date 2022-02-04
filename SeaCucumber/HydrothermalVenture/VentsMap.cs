using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarSweep.HydrothermalVenture
{
    public partial class VentsMap
    {
        public byte[,] Map { get; set; }
        private string path = @"HydrothermalVenture\LinesOfVents.txt";

        public VentsMap()
        {
            Map = new byte[1000,1000];
            foreach (var textLine in File.ReadLines(path))
            {
                if (textLine is not null)
                {
                    var line = new Line(textLine);
                    if (line.IsHorizontal || line.IsVertical)
                    {
                        DrawLine(line);
                    }
                }
            }
        }

        private void DrawLine(Line line)
        {
            if (line.IsVertical)
            {
                for (int i = Math.Min(line.P1.y, line.P2.y); i <= Math.Max(line.P1.y, line.P2.y); i++)
                {
                    Map[line.P1.x, i]++;
                }
            }
            else if(line.IsHorizontal)
            {
                for (int i = Math.Min(line.P1.x, line.P2.x); i <= Math.Max(line.P1.x, line.P2.x); i++)
                {
                    Map[i, line.P1.y]++;
                }
            }
        }

        public int CalcOverlappingLines()
        {
            int total = 0;
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    if(Map[i,j] > 1)
                    {
                        total++;
                    }
                }
            }
            return total;   
        }
    }
}
