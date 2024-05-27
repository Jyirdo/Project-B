using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestBase]
public class TestBaseLogic : IBaseLogic
{
    public List<TourModel> GetAllTours()
    {
        return new List<TourModel>
            {
                new TourModel { tourId = 1, dateTime = new DateTime(2023, 6, 1, 10, 0, 0) },
                new TourModel { tourId = 2, dateTime = new DateTime(2023, 6, 2, 14, 0, 0) }
            };
    }
}
[TestFixture]
public class TourLoaderTests
{
    [Test]
    public void Load_Tours_ShouldPrintToursToConsole()
    {
        IBaseLogic testBaseLogic = new TestBaseLogic();

        using (var SW = new StringWriter())
        {
            TourLoader.Load_Tours(testBaseLogic, SW);

            var expectedOutput = "Bij deze rondleidingen kunt u zich aanmelden:\n\n" +
                                "1: Rondleiding van 6/1/2023 10:00:00 AM\n" +
                                "2: Rondleiding van 6/2/2023 2:00:00 PM\n";
            Assert.AreEqual(expectedOutput, sw.ToString());
        }
    }
}