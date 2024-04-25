using FinalProject;

namespace FinalProjectTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TotalSalaryPilot_8000_9760returned()
        {
            double expected = 9760;
            Pilot p = new Pilot("Ada AA", 8000, "first", "top");
            double actual = p.CalculateSalary();

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void FuelCost_6h_18000returned()
        {
            double expected = 18000;
            int flightHours = 6;

            double actual = Program.CalculateFuelCost(flightHours);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TotalPriceTest()
        {
            List<Pilot> pList = new List<Pilot>();
            Pilot p1 = new Pilot("Test Test", 2350, "second", "mid");
            Pilot p2 = new Pilot("Tset Tset", 3450, "first", "mid");
            pList.Add(p1);
            pList.Add(p2);

            List<FlightAttendant> fList = new List<FlightAttendant>();
            FlightAttendant f1 = new FlightAttendant("Flight Attendant", 1700, "first", new List<string> { "E", "E", "E" });
            FlightAttendant f2 = new FlightAttendant("Flight Attendant2", 1360, "second", new List<string> { "E", "E" });
            fList.Add(f1);
            fList.Add(f2);
            
            int flightHours = 4;

            double fuelPrice = Program.CalculateFuelCost(flightHours);

            double expected = 24270;
            double actual = Program.CalculateTotalPrice(pList,fList,flightHours);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TotalSalaryAttendant_2500_3300returned()
        {
            double expected = 3300;
            FlightAttendant f = new FlightAttendant("AA ADA", 2500, "first", new List<string> { "E", "E", "E" });
            double actual = f.CalculateSalary();

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void PilotTier_4hours_MidReturned()
        {
            int flightHours = 4;
            string expected = "mid";
            string actual = Program.GetPilotTier(flightHours);

            Assert.AreEqual(expected, actual);
        }
    }
}