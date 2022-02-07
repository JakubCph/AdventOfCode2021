using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarSweep.TreacheryOfWhales
{
    public class CrabSimulation
    {
        public ushort[] CrabPositions { get; }
        private string path = @"TreacheryOfWhales\WhalesPositions.txt";
        public CrabSimulation()
        {
            CrabPositions = File.ReadAllText(path).Split(",").Select(s => ushort.Parse(s)).ToArray();
        }
        /// <summary>
        /// Fuel efficient horizontal position
        /// </summary>
        public (int position, int fuel) CalcHorizontalPos()
        {
            ushort maxPos = 0;
            int sumPrev = 0; 
            foreach (var pos in CrabPositions)
            {
                if(pos > maxPos) maxPos = pos;

                sumPrev += calculateFuel(pos);
            }

            int sumCurr = 0;
            for (int optimalPos = 1; optimalPos <= maxPos; optimalPos++)
            {
                for (int j = 0; j < CrabPositions.Length; j++)
                {
                    var posDiff = Math.Abs(CrabPositions[j] - optimalPos);
                    sumCurr += calculateFuel(posDiff);
                }
                
                if (sumCurr > sumPrev) 
                {
                    return (optimalPos - 1, sumPrev); 
                }
                else
                {
                    sumPrev = sumCurr;
                    sumCurr = 0;
                }
            }
            return (0,0);
        }
        private int calculateFuel(int posDiff)
        {
            return posDiff * (1 + posDiff) / 2;
        }
    }
}
