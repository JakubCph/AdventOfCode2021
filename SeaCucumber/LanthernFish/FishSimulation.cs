using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarSweep.LanthernFish
{
    public interface IFile 
    {
        string InputFile { get; }
    }
    public class FishSimulation : IFile
    {
        private string path = @"LanthernFish\FishInternalState.txt";

        public string InputFile { get; }

        IList<byte> FishPopulation;
        
        // will hold the count of the fish of internal timer being the index
        long[] FishPopulationSnapshot; 
        public int Count => FishPopulation.Count;
        public long CountLongSimulation => FishPopulationSnapshot.Aggregate((sum,e) => sum + e);
        public FishSimulation()
        {
            InputFile = File.ReadAllText(path);
            FishPopulation = new List<byte>();
            FishPopulationSnapshot = new long[9]; // there are 9 internal timer settings
        }

        private void InitFishPopulationSnapshot()
        {
            FishPopulationSnapshot = new long[9];
            foreach (var fish in ReadInitState(InputFile))
            {
                FishPopulationSnapshot[fish]++;
            }
        }

        private static List<byte> ReadInitState(string input)
        {
            return input.Split(",").Select(s => byte.Parse(s)).ToList();
        }

        public void SimulateForDays(int days)
        {
            FishPopulation = ReadInitState(InputFile);
            for (int i = 0; i < days; i++)
            {
                for (int j = 0; j < FishPopulation.Count; j++)
                {
                    if (FishPopulation[j] == 0)
                    {
                        FishPopulation[j] = 7; // reset the timer
                        FishPopulation.Add(9); // a newborn fish
                    }
                    FishPopulation[j]--;
                }
            }
        }

        /// <summary>
        /// Uses array which holds the count of the fish which have internal timer corresponding to array's index
        /// </summary>
        public void SimulateForLongDays(int days)
        {
            InitFishPopulationSnapshot();
            for (int i = 0;i < days; i++)
            {
                long childrenCount = FishPopulationSnapshot[0];
                for(int j = 0; j < FishPopulationSnapshot.Length - 1; j++)
                {
                    //reduce the internal timer of each fish by 1
                    FishPopulationSnapshot[j] = FishPopulationSnapshot[j + 1];
                }
                FishPopulationSnapshot[8] = childrenCount;
                FishPopulationSnapshot[6] += childrenCount;
            }
        }
    }
}
