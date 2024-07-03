namespace ProjectB;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void SysteemTestTourReserveren()
    {
        FakeWorld world = new()
        {
            Now = new DateTime(2024, 07, 02, 10, 0, 0),
            LinesToRead = new() { "123", "1", "", "456", "hetdepot2024!", "z" },
            Files = new()
            {
                { "Data/visitor_codes.txt", "123" },
                { "Data/GuideInfo.txt", "456 Job" },
                { "Data/Tours.json", "[{\"tour_id\": 1, \"tourStartTime\": \"2024-07-04T10:20:00\", \"parttakers\": 0, \"limit\": 13, \"guide\": null, \"tourVisitorList\": [], \"reservationsList\": [], \"tourStarted\": false}]" }	
            }
        };
        Program.World = world;

        Program.Main();

        string actual = world.LinesWritten[6];
        string expected = $"Succesvol gereserveerd voor de rondleiding van \x1b[32m{"04-7-2024 10:20"}\x1b[0m\n";
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void SysteemTestTourAnnuleren()
    {
        FakeWorld world = new()
        {
            Now = new DateTime(2024, 07, 02, 10, 0, 0),
            LinesToRead = new() { "123", "a", "", "456", "hetdepot2024!", "z" },
            Files = new()
            {
                { "Data/visitor_codes.txt", "123" },
                { "Data/Tours.json", "[{\"tour_id\": 1, \"tourStartTime\": \"2024-07-04T10:20:00\", \"parttakers\": 1, \"limit\": 13, \"guide\": null, \"tourVisitorList\": [], \"reservationsList\": [{\"Barcode\": \"123\"}], \"tourStarted\": false}]" }	
            }
        };
        Program.World = world;

        Program.Main();

        string actual = world.LinesWritten[6];
        string expected = $"Uw reservering voor de rondleiding van \x1b[32m{"04-7-2024 10:20"}\x1b[0m is geannuleerd. Nog een prettige dag verder!\n";
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void SysteemTestStaffStartTour()
    {
        FakeWorld world = new()
        {
            Now = new DateTime(2024, 07, 02, 10, 0, 0),
            LinesToRead = new() { "456", "hetdepot2024!", "1", "123", "k", "s", "", "456", "hetdepot2024!", "z" },
            Files = new()
            {
                { "Data/visitor_codes.txt", "123" },
                { "Data/GuideInfo.txt", "456 Job" },
                { "Data/Tours.json", "[{\"tour_id\": 1, \"tourStartTime\": \"2024-07-04T10:20:00\", \"parttakers\": 1, \"limit\": 13, \"guide\": null, \"tourVisitorList\": [], \"reservationsList\": [{\"Barcode\": \"123\"}], \"tourStarted\": false}]" }	
            }
        };
        Program.World = world;

        Program.Main();

        string actual = world.LinesWritten[24];
        string expected = $"\x1b[32;1mDe rondleiding is succesvol gestart.\x1b[0m";
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void SysteemTestStaffStartTourWithLastMinuteExtraVisitor()
    {
        FakeWorld world = new()
        {
            Now = new DateTime(2024, 07, 02, 10, 0, 0),
            LinesToRead = new() { "456", "hetdepot2024!", "1", "123", "5912997400794", "k", "s", "", "456", "hetdepot2024!", "z" },
            Files = new()
            {
                { "Data/visitor_codes.txt", "123" },
                { "Data/GuideInfo.txt", "456 Job" },
                { "Data/Tours.json", "[{\"tour_id\": 1, \"tourStartTime\": \"2024-07-04T10:20:00\", \"parttakers\": 1, \"limit\": 13, \"guide\": null, \"tourVisitorList\": [], \"reservationsList\": [{\"Barcode\": \"123\"}], \"tourStarted\": false}]" }	
            }
        };
        Program.World = world;

        Program.Main();

        string actual = world.LinesWritten[29];
        string expected = $"\x1b[32;1mDe rondleiding is succesvol gestart.\x1b[0m";
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void SysteemTestStaffChecksVisitorWithWrongBarcode()
    {
        FakeWorld world = new()
        {
            Now = new DateTime(2024, 07, 02, 10, 0, 0),
            LinesToRead = new() { "456", "hetdepot2024!", "1", "1234", "k", "s", "", "456", "hetdepot2024!", "z" },
            Files = new()
            {
                { "Data/visitor_codes.txt", "123" },
                { "Data/GuideInfo.txt", "456 Job" },
                { "Data/Tours.json", "[{\"tour_id\": 1, \"tourStartTime\": \"2024-07-04T10:20:00\", \"parttakers\": 1, \"limit\": 13, \"guide\": null, \"tourVisitorList\": [], \"reservationsList\": [{\"Barcode\": \"123\"}], \"tourStarted\": false}]" }	
            }
        };
        Program.World = world;

        Program.Main();

        string actual = world.LinesWritten[15];
        string expected = $"U heeft een incorrecte invoer opgegeven, probeer het opnieuw.";
        Assert.AreEqual(expected, actual);
    }
}
