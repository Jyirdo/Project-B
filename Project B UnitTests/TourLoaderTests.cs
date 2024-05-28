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
            Tour.Initialize(testBaseLogic);
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

        [TestMethod]
        public void CheckIfReservation_ValidBarcode_ShouldReturnTrue()
        {
            bool result = Tour.CheckIfReservation(1234567890);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckIfReservation_InvalidBarcode_ShouldReturnFalse()
        {
            bool result = Tour.CheckIfReservation(5555555555);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ChooseTour_ValidTourId_ShouldReturnSuccessMessage()
        {
            string input = "1";
            using (var inputReader = new StringReader(input))
            {
                Console.SetIn(inputReader);
                using (var sw = new StringWriter())
                {
                    Console.SetOut(sw);
                    long barcode = 1234567890;

                    string result = TourLoader.ChooseTour(barcode);

                    var expectedOutput = $"Succesvol aangemeld bij de rondleiding van 01-6-2023 10:00\n";
                    Assert.AreEqual(expectedOutput, result);
                }
            }
        }

        [TestMethod]
        public void ChooseTour_InvalidTourId_ShouldReturnErrorMessage()
        {
            // Arrange
            string input = "99\n1";
            using (var inputReader = new StringReader(input))
            {
                Console.SetIn(inputReader);
                using (var sw = new StringWriter())
                {
                    Console.SetOut(sw);
                    long barcode = 1234567890;

                    TourLoader.ChooseTour(barcode);

                    var expectedOutput1 = "U heeft een incorrect tournummer opgegeven, probeer het opnieuw.";
                    Assert.IsTrue(sw.ToString().Contains(expectedOutput1));
                }
            }
        }

        [TestMethod]
        public void ChooseTour_FullTour_ShouldReturnFullMessage()
        {
            // Arrange
            string input = "2";
            using (var inputReader = new StringReader(input))
            {
                Console.SetIn(inputReader);
                using (var sw = new StringWriter())
                {
                    Console.SetOut(sw);
                    long barcode = 1234567890;

                    var baseLogic = new TestBaseLogic();
                    var tours = baseLogic.GetAllTours();
                    tours[1].parttakers = tours[1].limit;

                    TourLoader.Initialize(baseLogic);
                    TourLoader.ChooseTour(barcode);

                    var expectedOutput = "Deze tour is helaas vol, probeer een andere optie.\n";
                    Assert.IsTrue(sw.ToString().Contains(expectedOutput));
                }
            }
        }
    }
}
