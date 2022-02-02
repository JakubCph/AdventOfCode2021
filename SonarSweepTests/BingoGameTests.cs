using NUnit.Framework;
using SonarSweep.BingoGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarSweepTests
{
    public class BingoGameTests
    {
        [Test]
        public void BingoGame_WhenCtorCalled_ThenSetsSequence()
        {
            var game = new BingoGame();
            Assert.IsTrue(game.SeqOfNumbers.Length > 0);
        }

        [Test]
        public void BingoGame_WhenCtorCalled_ThenBoardsSet()
        {
            var game = new BingoGame();
            Assert.IsTrue(game.Boards.Count > 0);
        }
    }
}
