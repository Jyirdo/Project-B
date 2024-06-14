namespace ProjectB;

[TestClass]
public class UnitTest1
{
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
        Assert.AreEqual("Goedennacht, ", resultNight2);
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

    [TestMethod]
    public void TestCorrectStaffCode_True()
    {
        string staffcode = "456";

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
        string barcode = "2";
        string date = DateTime.Now.ToString("yyyy/MM/dd") + "T10:20";
        DateTime time = DateTime.Parse(date);
        Tour tour = new Tour(tourId, time);

        string expected = $"{barcode} succesvol aangemeld bij de rondleiding van {tour.tourStartTime.ToString("dd-M-yyyy HH:mm")}\n";
        string actual = Staff.AddLastMinuteVisitor(tourId, barcode);

        Assert.AreEqual(expected, actual);
        Add_Remove.Remove(new Visitor(barcode), 1);
    }

    [TestMethod]
    public void TestCheckIfReservation_True()
    {
        string barcode = "3";
        Add_Remove.Add(new Visitor(barcode), 1);

        string expected = "true";
        string actual = Tour.CheckIfReservation(barcode);

        Assert.AreEqual(expected, actual);
        Add_Remove.Remove(new Visitor(barcode), 1);
    }

    [TestMethod]
    public void TestCheckIfReservation_False()
    {
        string barcode = "0000000000";
        Add_Remove.Remove(new Visitor(barcode), 1);

        string expected = "false";
        string actual = Tour.CheckIfReservation(barcode);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestGetTourTime_True()
    {
        string barcode = "4";
        int tourId = 1;
        Add_Remove.Add(new Visitor(barcode), tourId);
        string date = DateTime.Now.ToString("yyyy/MM/dd") + "T10:20";
        DateTime time = DateTime.Parse(date);
        Tour tour = new Tour(tourId, time);

        string expected = $"U heeft een rondleiding geboekt om \x1b[32m{tour.tourStartTime}\x1b[0m\n";
        string actual = Tour.GetTourTime(barcode, false);

        Assert.AreEqual(expected, actual);
        Add_Remove.Remove(new Visitor(barcode), 1);
    }

    [TestMethod]
    public void TestGetTourTime_False()
    {
        string barcode = "5";
        Tour.CancelReservation(barcode);

        string expected = "U heeft nog geen rondleiding geboekt\n";
        string actual = Tour.GetTourTime(barcode, false);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestCancelReservation_True()
    {
        string barcode = "6";
        int tourId = 1;
        Add_Remove.Add(new Visitor(barcode), tourId);
        string date = DateTime.Now.ToString("yyyy/MM/dd") + "T10:20";
        DateTime time = DateTime.Parse(date);
        Tour tour = new Tour(tourId, time);

        string expected = $"Uw tour van \x1b[32m{tour.tourStartTime}\x1b[0m is geannuleerd. Nog een prettige dag verder!";
        string actual = Tour.CancelReservation(barcode);

        Assert.AreEqual(expected, actual);
        Add_Remove.Remove(new Visitor(barcode), 1);
    }

    [TestMethod]
    public void TestCancelReservation_False()
    {
        string barcode = "7";
        Tour.CancelReservation(barcode);

        string expected = "U heeft nog geen tour ingepland\n";
        string actual = Tour.CancelReservation(barcode);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void SysteemTestTourReserveren()
    {
        string date = DateTime.Now.ToString("yyyy/MM/dd") + "T10:20:00";
        FakeWorld world = new()
        {
            LinesToRead = new() { "123", "1" }
        };
        TestMenu menu = new(world);
        menu.MainMenu();

        string actual = world.LinesWritten.Last();
        string expected = $"Succesvol aangemeld bij de rondleiding van \x1b[32m{DateTime.Today.ToString("dd-M-yyyy")} 10:20\x1b[0m\n";
        Assert.AreEqual(expected, actual);
        
        Visitor visitor= new Visitor("123");
        Add_Remove.Remove(visitor, 1);
    }

    [TestMethod]
    public void SysteemTestTourAnnuleren()
    {
        Visitor visitor= new Visitor("123");
        Add_Remove.Add(visitor, 1);

        FakeWorld world = new()
        {
            LinesToRead = new() { "a" }
        };
        TestMenu menu = new(world);
        menu.SubMenu("123");
        string date = DateTime.Now.ToString("yyyy/MM/dd") + "T10:20:00";

        string actual = world.LinesWritten.Last();
        string expected = $"Uw tour van \x1b[32m{DateTime.Today.ToString("dd-M-yyyy")} 10:20:00\x1b[0m is geannuleerd. Nog een prettige dag verder!";
        Assert.AreEqual(expected, actual);
    }

    // [TestMethod]
    // public void SysteemTestGidsLogin()
    // {
    //     FakeWorld world = new()
    //     {
    //         LinesToRead = new() { "1234567891011", "hetdepot2024!" }
    //     };
    //     TestMenu menu = new(world);
    //     menu.MainMenu();

    //     string actual = world.LinesWritten.Last();
    //     string expected = $"Uw tour van \x1b[32m{DateTime.Today.ToString("dd-M-yyyy")} 10:20:00\x1b[0m is geannuleerd. Nog een prettige dag verder!";
    //     Assert.AreEqual(expected, actual);
    // }
}
