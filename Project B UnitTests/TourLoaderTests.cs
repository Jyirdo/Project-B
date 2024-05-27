using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class TourLoaderTests
{
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
}
