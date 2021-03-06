using NUnit.Framework;
using SonarSweep;

namespace SonarSweepTests
{
    public class DiagnosticTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CalculateParameters_WhenCalled_ThenSetsParameters()
        {
            var diag = new Diagnostic();
            diag.CalculateParameters();
            int expectedPower = 2003336;
            Assert.AreEqual(expectedPower, diag.Gamma * diag.Epsilon);
        }

        [Test]
        public void CalculateLifeSupportRating_WhenCalled_ThenReturnsTheRating()
        {
            var diag = new Diagnostic();
            long actual = diag.CalculateLifeSupportRating();

            long expected = 1877139;
            Assert.AreEqual(expected, actual);

        }
    }
}