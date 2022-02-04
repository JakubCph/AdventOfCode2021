// See https://aka.ms/new-console-template for more information
using SonarSweep;
using SonarSweep.BingoGame;
using SonarSweep.HydrothermalVenture;
using SonarSweep.LanthernFish;
using static SonarSweep.Pilot;

Console.WriteLine("Hello, World!");

IList<int> inputDepths = new List<int>();
foreach (var line in File.ReadLines(@"DepthsScanReport.txt"))
{
    inputDepths.Add(int.Parse(line));   
}
int numberOfIncreasedDepths = Calculate3MeasurementSlidingWindow(inputDepths).Count(e => e.Equals(DepthChange.Increased));

Console.WriteLine($"Increased depths: {numberOfIncreasedDepths}");

/* Sonar Sweep */
var pilot = new Pilot();
foreach(string? line in File.ReadLines(@"PlannedCourse.txt"))
{
    if(line is not null)
    {
        var parts= line.Split(' ');
        Direction direction = (Direction) Enum.Parse(typeof(Direction), parts[0]);
        int step = int.Parse(parts[1]);

        pilot.Move2(direction, step);
    }
}
Console.WriteLine($"Horizontal position * depth = {pilot.HorizontalPosition * pilot.Depth}");


var diag = new Diagnostic();
diag.CalculateParameters();
Console.WriteLine($"Power consumption is {diag.Gamma * diag.Epsilon}");

var bingo = new BingoGame();
var result = bingo.Play();
Console.WriteLine($"The winning board scored {result}");

bingo = new BingoGame();
result = bingo.PlayForLastWinningBoard();
Console.WriteLine($"The last winning board scored {result}");

var ventsMap = new VentsMap();
var overlapping = ventsMap.CalcOverlappingLines();
Console.WriteLine($"Overlapping vents are {overlapping}");

var fishSchool = new FishSimulation();
fishSchool.SimulateForDays(80);
fishSchool.SimulateForDays(80);
Console.WriteLine($"After 80 days is {fishSchool.Count}");
fishSchool.SimulateForLongDays(256);
Console.WriteLine($"After 256 days is {fishSchool.CountLongSimulation}");

static IList<bool>? CalculateIncreasedDepths(IList<int> depths)
{
    if(depths is null || depths.Count < 2) return null;

    List<bool> result = new();  
    for (int i = 1; i < depths.Count; i++)
    {
        result.Add(depths[i] >= depths[i-1]);
    }
    return result;
}
static IList<DepthChange> Calculate3MeasurementSlidingWindow(IList<int> input)
{
    if (input is null || input.Count < 4) return new List<DepthChange>();

    List<DepthChange> result = new();
    int lastSum = 0;
    int secondLastSum = input[0] + input[1] + input[2];
    for (int idxL = 0, idxR = 3; idxR < input.Count; idxL++, idxR++)
    {
        lastSum = secondLastSum + input[idxR] - input[idxL];
        result.Add(CompareSums(secondLastSum, lastSum));
        secondLastSum = lastSum;    
    }
    return result;
}

static DepthChange CompareSums(int secondLastSum, int lastSum)
{
    int comparison = lastSum.CompareTo(secondLastSum);
    return comparison switch
    {
        0 => DepthChange.NoChange,
        > 0 => DepthChange.Increased,
        < 0 => DepthChange.Decreased
    };
}

enum DepthChange
{
    Increased,
    Decreased,
    NoChange
}