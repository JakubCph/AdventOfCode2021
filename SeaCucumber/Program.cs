// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

IList<int> inputDepths = new List<int>();
foreach (var line in File.ReadLines(@"DepthsScanReport.txt"))
{
    inputDepths.Add(int.Parse(line));   
}
int numberOfIncreasedDepths = Calculate3MeasurementSlidingWindow(inputDepths).Count(e => e.Equals(Depth.Increased));

Console.WriteLine($"Increased depths: {numberOfIncreasedDepths}");

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
static IList<Depth> Calculate3MeasurementSlidingWindow(IList<int> input)
{
    if (input is null || input.Count < 4) return new List<Depth>();

    List<Depth> result = new();
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

static Depth CompareSums(int secondLastSum, int lastSum)
{
    int comparison = lastSum.CompareTo(secondLastSum);
    return comparison switch
    {
        0 => Depth.NoChange,
        > 0 => Depth.Increased,
        < 0 => Depth.Decreased
    };
}

enum Depth
{
    Increased,
    Decreased,
    NoChange
}