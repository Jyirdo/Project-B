[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
    }

    [TestMethod]
    public void VisitorTest()
    {
        long expectedBarcode = 1234567890;

        Visitor visitor = new Visitor(expectedBarcode);

        Assert.AreEqual(expectedBarcode, visitor.barcode);
    }

    [TestMethod]
    public void TestShowGreeting_Night()
    {
        int hourNight = 5;
        int hourMorning = 6;

        string resultNight = Greeting.ShowGreeting(hourNight);
        string resultMorning = Greeting.ShowGreeting(hourMorning);

        Assert.AreEqual("Goedennacht, ", resultNight);
        Assert.AreEqual("Goedemorgen, ", resultMorning);
    }

    [TestMethod]
    public void TestShowGreeting_Afternoon()
    {
        int hourMorning = 11;
        int hourAfternoon = 12;

        string resultMorning = Greeting.ShowGreeting(hourMorning);
        string resultAfternoon = Greeting.ShowGreeting(hourAfternoon);

        Assert.AreEqual("Goedemorgen, ", resultMorning);
        Assert.AreEqual("Goedemiddag, ", resultAfternoon);
    }

    [TestMethod]
    public void TestShowGreeting_Morning()
    {
        int hourAfternoon = 17;
        int hourEvening = 18;

        string resultAfternoon = Greeting.ShowGreeting(hourAfternoon);
        string resultEvening = Greeting.ShowGreeting(hourEvening);

        Assert.AreEqual("Goedemiddag, ", resultAfternoon);
        Assert.AreEqual("Goedenavond, ", resultEvening);
    }

    [TestMethod]
    public void TestShowGreeting_Evening()
    {
        int hourEvening = 23;
        int hourNight = 24;
        int hourNight2 = 5;

        string resultEvening = Greeting.ShowGreeting(hourEvening);
        string resultNight = Greeting.ShowGreeting(hourNight);
        string resultNight2 = Greeting.ShowGreeting(hourNight2);

        Assert.AreEqual("Goedenavond, ", resultEvening);
        Assert.AreEqual("Goedennacht, ", resultNight);
        Assert.AreEqual("Goedennacht, ", resultNight);
    }

    [TestMethod]
    public void TestShowGreeting_Welkom()
    {
        int hourInvalid = -1;
        int hourInvalid2 = 25;

        string resultInvalid = Greeting.ShowGreeting(hourInvalid);
        string resultInvalid2 = Greeting.ShowGreeting(hourInvalid2);

        Assert.AreEqual("Welkom, ", resultInvalid);
        Assert.AreEqual("Welkom, ", resultInvalid2);
    }

    // [TestMethod]
    // public void TestChooseTour()
    // {
    //     int tourId = 1;
    //     long barcode = 1;
    //     DateTime time = DateTime.Parse("2024-04-10T10:20:00");
    //     Tour tour = new Tour(tourId, time);

    //     string expected = $"Succesvol aangemeld bij de rondleiding van {tour.tourStartTime.ToString("dd-M-yyyy HH:mm")}\n";
    //     string actual = Tour.ChooseTour(barcode, "1");

    //     Assert.AreEqual(expected, actual);
    // }

    [TestMethod]
    public void TestCorrectStaffCode_True()
    {
        string staffcode = "1234567891011";

        bool expected = true;
        bool actual = Staff.CorrectStaffCode(staffcode);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestCorrectStaffCode_False()
    {
        string staffcode = "0000000000";

        bool expected = false;
        bool actual = Staff.CorrectStaffCode(staffcode);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestAddLastMinuteVisitor()
    {
        int tourId = 1;
        string input = "2";
        DateTime time = DateTime.Parse("2024-04-10T10:20:00");
        Tour tour = new Tour(tourId, time);

        string expected = $"{input} succesvol aangemeld bij de rondleiding van {tour.tourStartTime.ToString("dd-M-yyyy HH:mm")}\n";
        string actual = Staff.AddLastMinuteVisitor(tourId, input);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestCheckIfReservation_True()
    {
        long barcode = 3;
        Add_Remove.Add(new Visitor(barcode), 1);

        bool expected = true;
        bool actual = Tour.CheckIfReservation(barcode);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestCheckIfReservation_False()
    {
        long barcode = 00000000;
        Add_Remove.Remove(new Visitor(barcode), 1);

        bool expected = false;
        bool actual = Tour.CheckIfReservation(barcode);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestGetTourTime_True()
    {
        long barcode = 4;
        int tourId = 1;
        Add_Remove.Add(new Visitor(barcode), tourId);
        DateTime time = DateTime.Parse("2024-04-10T10:20:00");
        Tour tour = new Tour(tourId, time);

        string expected = $"{tour.tourStartTime.ToString("dd-M-yyyy HH:mm")}";
        string actual = Tour.GetTourTime(barcode);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestGetTourTime_False()
    {
        long barcode = 5;
        Tour.CancelReservation(barcode);

        string expected = "U heeft geen rondleiding geboekt";
        string actual = Tour.GetTourTime(barcode);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestCancelReservation_True()
    {
        long barcode = 6;
        int tourId = 1;
        Add_Remove.Add(new Visitor(barcode), tourId);
        DateTime time = DateTime.Parse("2024-04-10T10:20:00");
        Tour tour = new Tour(tourId, time);

        string expected = $"Uw tour van {tour.tourStartTime} is geannuleerd";
        string actual = Tour.CancelReservation(barcode);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestCancelReservation_False()
    {
        long barcode = 7;
        Tour.CancelReservation(barcode);

        string expected = "U heeft nog geen tour ingepland";
        string actual = Tour.CancelReservation(barcode);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void SysteemTestTourReserveren()
    {
        FakeWorld fakeworld = new();
        Program.World = fakeworld;
        int tourId = 1;
        DateTime time = DateTime.Parse("2024-04-10T10:20:00");
        Tour tour = new Tour(tourId, time);

        fakeworld.LinesToRead.Add("1");
        fakeworld.LinesToRead.Add("1");
        string actual = fakeworld.LinesWritten.Last();
        string expected = $"Succesvol aangemeld bij de rondleiding van \x1b[32m{tour.tourStartTime.ToString("dd-M-yyyy HH:mm")}\x1b[0m\n";
        Assert.AreEqual(expected, actual);
    }

}
