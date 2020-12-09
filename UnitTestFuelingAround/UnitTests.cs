using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseWork;

namespace UnitTestFuelingAround
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void Calculate_Litres_Dispensed()
        {
            //Arrange
            int MaxCapacity = 50;
            int StartCapacity = 2;
            int expected = 48;

            // Act

            AllPumps allPumps = new AllPumps(9);
            int value = allPumps.CalculateLitresDispensed(MaxCapacity, StartCapacity);
            Assert.AreEqual(expected, value, "Incorrect calculation");
        }

        [TestMethod]
        public void Calculate_Litres_Dispensed_BadInput()
        {
            //Arrange
            int MaxCapacity = 50;
            int StartCapacity = 2;
            int expected = 43;

            // Act

            AllPumps allPumps = new AllPumps(9);
            int value = allPumps.CalculateLitresDispensed(MaxCapacity, StartCapacity);
            Assert.AreNotEqual(expected, value, "Incorrect calculation");
        }

        [TestMethod]
        public void Calculate_Time_Fuel()
        {
            //Arrange
            int MaxCapacity = 80;
            int StartCapacity = 23;
            int expected = 38000;

            // Act

            AllPumps allPumps = new AllPumps(9);
            int value = allPumps.CalculateTimeToFuel(MaxCapacity, StartCapacity);
            Assert.AreEqual(expected, value, "Incorrect calculation");
        }

        [TestMethod]
        public void Calculate_Time_Fuel_BadInput()
        {
            //Arrange
            int MaxCapacity = 80;
            int StartCapacity = 23;
            int expected = 32356;

            // Act

            AllPumps allPumps = new AllPumps(9);
            int value = allPumps.CalculateTimeToFuel(MaxCapacity, StartCapacity);
            Assert.AreNotEqual(expected, value, "Incorrect calculation");
        }



    }
}
