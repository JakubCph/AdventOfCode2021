using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarSweep.BingoGame
{
    public class Board
    {
        public ushort[,] Values { get; set; }
        public ushort[,] Matches{ get; set; }
        public int Size { get; set; }
        public bool Won { get; set; } = false;

        public Board(string[] values)
        {
            Size = values.Length;
            Values = new ushort[Size, Size];
            for(int row = 0; row < Size; row++)
            {
                ushort[]? seq = values[row].Split(" ").Where(x => !string.IsNullOrEmpty(x)).Select(c => ushort.Parse(c)).ToArray();  
                if(seq is not null)
                {
                    for(int col = 0; col< Size; col++)
                    {
                        Values[row, col] = seq[col];
                    }
                }
            }
            Matches = new ushort[Size, Size];  
        }

        public bool MarkNumber(ushort number)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if(Values[i,j] == number)
                    {
                        Matches[i, j] = 1;
                        bool bingo = IsBingo(i,j);
                        if (bingo)
                        {
                            Won = true;
                        }
                        return bingo;
                    }
                }
            }
            return false;
        }

        private bool IsBingo(int row, int col)
        {
            bool fullRow = true;
            bool fullCol = true;

            for (int i = 0; i < Size; i++)
            {
                if(Matches[row,i] == 0) fullRow = false;
            }

            for (int i = 0; i < Size; i++)
            {
                if(Matches[i,col] == 0) fullCol = false;
            }
            return fullCol || fullRow;
        }

        public int CalculateScore(int lastCalledNumber)
        {
            int sumOfUnmarked = FindSumOfUnmarked();
            return sumOfUnmarked * lastCalledNumber;
        }

        private int FindSumOfUnmarked()
        {
            int sum = 0;
            for (int i = 0;i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if(Matches[i,j] == 0)
                    sum += Values[i,j];
                }
            }
            return sum;
        }

    }
}
