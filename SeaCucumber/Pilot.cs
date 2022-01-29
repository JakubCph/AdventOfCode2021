using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarSweep
{
    internal class Pilot
    {
        public int HorizontalPosition { get; set; }
        public int Depth { get; set; }
        public int Aim { get; set; }

        public enum Direction
        {
            forward,
            down,
            up
        }

        public void Move(Direction dir, int step)
        {
            switch (dir)
            {
                case Direction.forward:
                    HorizontalPosition += step;
                    break;
                case Direction.up:
                    Depth -= step;
                    break;
                case Direction.down:
                    Depth += step;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public void Move2(Direction dir, int step)
        {
            switch (dir)
            {
                case Direction.forward:
                    HorizontalPosition += step;
                    Depth += Aim * step;
                    break;
                case Direction.up:
                    Aim -= step;
                    break;
                case Direction.down:
                    Aim += step;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
