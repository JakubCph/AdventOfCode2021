using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarSweep.BingoGame
{
    public class BingoGame
    {
        public ushort[] SeqOfNumbers;
        public IList<Board> Boards;
        private string path = @"BingoGame\BingoInput.txt";
        public BingoGame()
        {
            var input = File.ReadAllLines(path);
            SeqOfNumbers = input[0].Split(",").Select(c => ushort.Parse(c)).ToArray();

            Boards = new List<Board>();
            var boardInput = new List<string>();
            for (int i = 2; i < input.Length; i++)
            {
                if (string.IsNullOrEmpty(input[i]))
                {
                    Boards.Add(new Board(boardInput.ToArray()));
                    boardInput.Clear();
                    continue;
                }

                boardInput.Add(input[i]);
            }
        }

        private Board FirstWinningBoard { get; set; }
        private Board LastWinningBoard { get; set; }
        private ushort LastWinningNumber { get; set; }
        public int Play()
        {
            for (int i = 0; i < SeqOfNumbers.Length; i++)
            {
                for (int j = 0; j < Boards.Count; j++)
                {
                    if (Boards[j].MarkNumber(SeqOfNumbers[i]))
                    {
                        FirstWinningBoard = Boards[j];
                        return FirstWinningBoard.CalculateScore(SeqOfNumbers[i]);
                    }
                }
            }
            return 0;
        }

        public int PlayForLastWinningBoard()
        {
            // 1 case: run all number sequence and find that not all boards are winning
            // 2 case: all the borads are winning and we did not ran of out numbers in sequence
            
            for (int i = 0; i < SeqOfNumbers.Length; i++)
            {
                for (int j = 0; j < Boards.Count; j++)
                {
                    //excludes the boards which already won from the game
                    if (!Boards[j].Won && Boards[j].MarkNumber(SeqOfNumbers[i]))
                    {
                        LastWinningBoard = Boards[j];
                        LastWinningNumber = SeqOfNumbers[i];
                    }
                }
            }
            return LastWinningBoard.CalculateScore(LastWinningNumber);
        }
    }
}
