using Moq;
using NUnit.Framework;
using SonarSweep.LanthernFish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarSweepTests
{
    public class FishSimulationTests
    {
       
        [Test]
        public void SimulateForDays_WhenCalledWithX_ReturnsFishPopulationAfterXDays()
        {
            //var FishSimMock = new Mock<IFile>();
            //FishSimMock.SetupGet(x => x.InputFile).Returns("1,1");

            var simulation = new FishSimulation();
            simulation.SimulateForDays(80);

            Assert.AreEqual(352195, simulation.Count);
            
        }
    }
}
