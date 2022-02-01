using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarSweep
{
    public class Diagnostic
    {
        public int Gamma { get; set; } 
        public int Epsilon { get; set; }

        private int binarySize = 12;

        private string diagnosticsFile = @"DiagnosticsReport.txt";
        private int invertMask = 0xfff;
        public void CalculateParameters()
        {
            int lineCount = 0;
            int[] onesCount = new int[binarySize];   
            foreach(string? line in File.ReadLines(diagnosticsFile))
            {
                if(line is not null)
                {
                    lineCount++;
                    for (int i = 0; i < binarySize; i++)
                    {
                        if(line[i] == '1')
                        {
                            onesCount[i]++; 
                        }
                    }
                }
            }

            for (int pos = 0; pos < binarySize; pos++)
            {
                if(onesCount[pos] > lineCount / 2)
                {
                    Gamma |= 1 << (binarySize - 1 - pos); //set bit a position
                }
            }
            Epsilon = Gamma ^ invertMask;

        }

        //public void CalculateLifeSupportingRate()
        //{
        //    IList<ushort>? report = ReadInReport();
        //    int OxygenGenRate = FindOxygenGeneratorRate(report);
        //}

        //private int FindOxygenGeneratorRate(IList<ushort> report)
        //{

        //}

        private IList<ushort> ReadInReport()
        {
            var report = new List<ushort>();
            foreach (string? line in File.ReadLines(diagnosticsFile))
            {
                if (line is not null)
                {
                    report.Add(ushort.Parse(line));
                }
            }
            return report;
        }
    }
}
