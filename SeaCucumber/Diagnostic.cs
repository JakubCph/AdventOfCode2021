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

        public long CalculateLifeSupportRating()
        {
            string[]? report = File.ReadAllLines(diagnosticsFile);
            if (report is null) throw new Exception("Report is empty") ;

            int OxygenRating = CalculateRating(report, RatingType.OxygenGenRating);
            int CO2Rating = CalculateRating(report, RatingType.CO2ScrubberRating);
            return OxygenRating * CO2Rating;
        }

        enum RatingType
        {
            OxygenGenRating,
            CO2ScrubberRating
        }
        private int CalculateRating(string[] report, RatingType type)
        {
            string[] reportCopy = CopyArray(report);

            int bitPos = 0;
            while (RatingNotFound(reportCopy))
            {
                //find most common bit
                char bit = FindMostOrLeastCommonBit(type == RatingType.OxygenGenRating, reportCopy, bitPos);
                //filter report
                reportCopy = reportCopy.Where(b => b[bitPos] == bit).ToArray();
                //move to the next bit
                bitPos++;
            }
            return Convert.ToInt32(reportCopy[0], 2);
        }

        private char FindMostOrLeastCommonBit(bool most, string[] array, int bitPos)
        {
            int len = array.Length;
            int zeros = 0;
            int ones = 0;
            
            int i = 0;
            while (i < len && Math.Max(zeros,ones) <= len/2)
            {
                if(array[i][bitPos] == '0') zeros++;
                else ones++;

                i++;
            }
            
            if (most)
            {
                return (ones >= zeros) ? '1' : '0';
            }
            else
            {
                return (zeros <= ones) ? '0' : '1';
            }
        }

        private static string[] CopyArray(string[] report)
        {
            int arrL = report.Length;
            string[] reportCopy = new string[arrL];
            Array.Copy(report, reportCopy, arrL);
            return reportCopy;
        }

        private static bool RatingNotFound(string[] reportCopy)
        {
            return reportCopy.Length > 1;
        }
    }
}
