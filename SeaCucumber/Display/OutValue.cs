using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarSweep.Display
{
    public class OutValue
    {
        public Dictionary<int, int> Digits =  new()
        {
            { 1, 2},
            { 4, 4},
            { 7, 3 },
            { 8, 7}
        };
        public OutValue(string txt)
        {
            string[] parts = txt.Trim().Split(" ");
            int[] charCount = parts.Select(x => x.Length).ToArray();
            DigitCount = charCount.Where(d => Digits.ContainsValue(d)).Count();
        }
        public int DigitCount { get; init; }
    }
}
