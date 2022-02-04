namespace SonarSweep.HydrothermalVenture
{
    public partial class VentsMap
    {
        public class Line
        {
            public (int x, int y) P1{ get; set; }
            public (int x, int y) P2{ get; set; }
            //assumption: the points can only be on the positive first quadrant
            public Line(string lineText)
            {
                var coordinates = lineText.Split(" -> ");
                var pair1 = coordinates[0].Split(",").Select(s => int.Parse(s)).ToArray();
                var pair2 = coordinates[1].Split(",").Select(s => int.Parse(s)).ToArray();
                
                P1 = (pair1[0], pair1[1]);
                P2 = (pair2[0], pair2[1]);
            }

            public bool IsHorizontal => P1.y == P2.y;
            public bool IsVertical => P1.x == P2.x;
            public bool IsDiagonal => Math.Abs(P1.x - P2.x) == Math.Abs(P1.y - P2.y); // angle 45 deg - square
        };
    }
}
