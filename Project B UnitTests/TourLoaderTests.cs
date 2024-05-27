using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Logic;
using Project.Models;

namespace Project.Tests
{
    [TestClass]
    public class TourLoaderTests
    {
        [TestInitialize]
        public void Setup()
        {
            TourLoader.baseLogic = new TestBaseLogic();
        }

        [TestMethod]
        public void Load_Tours_ShouldPrintToursToConsole()
        {
            IBaseLogic testBaseLogic = new TestBaseLogic();

            using (var sw = new StringWriter())
            {
                TourLoader.Load_Tours(testBaseLogic, sw);

                var expectedOutput = "Bij deze rondleidingen kunt u zich aanmelden:\n\n" +
                                     "1: Rondleiding van 6/1/2023 10:00:00 AM\n" +
                                     "2: Rondleiding van 6/2/2023 2:00:00 PM\n";
                Assert.AreEqual(expectedOutput, sw.ToString());
            }
        }

        [TestMethod]
        public void GetTourTime_ShouldReturnCorrectTourTime_ForExistingBarcode()
        {
            long barcode = 1234567890;

            string result = TourLoader.GetTourTime(barcode);

            Assert.AreEqual("01-6-2023 10:00", result);
        }

        [TestMethod]
        public void GetTourTime_ShouldReturnNoBookingMessage_ForNonExistingBarcode()
        {
            long barcode = 1111111111;

            string result = TourLoader.GetTourTime(barcode);

            Assert.AreEqual("U heeft geen rondleiding geboekt", result);
        }
    }
}
