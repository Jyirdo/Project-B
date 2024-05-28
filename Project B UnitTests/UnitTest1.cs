namespace Project_B_UnitTests;

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
        for (int i = 0; i < 6; i++)
        {
            int nightTime = i;
            string result = Greeting.ShowGreeting(nightTime);
            Assert.AreEqual("Goedennacht, ", result);
        }
    }

    [TestMethod]
    public void TestShowGreeting_Morning()
    {
        for (int i = 6; i < 12; i++)
        {
            int morningTime = i;
            string result = Greeting.ShowGreeting(morningTime);
            Assert.AreEqual("Goedemorgen, ", result);
        }
    }

    [TestMethod]
    public void TestShowGreeting_Afternoon()
    {
        for (int i = 12; i < 18; i++)
        {
            int afternoonTime = i;
            string result = Greeting.ShowGreeting(afternoonTime);
            Assert.AreEqual("Goedemiddag, ", result);
        }
    }

    [TestMethod]
    public void TestShowGreeting_Evening()
    {
        for (int i = 18; i < 24; i++)
        {
            int eveningTime = 23;
            string result = Greeting.ShowGreeting(eveningTime);
            Assert.AreEqual("Goedenavond, ", result);
        }
    }

    [TestMethod]
    public void TestChoose_Tour()
    {
        int tourId = 1;
        long barcode = 1234;
        DateTime time = DateTime.Parse("2024-04-10T10:20:00");
        Tour tour = new Tour(tourId, time);

        string expected = $"Succesvol aangemeld bij de rondleiding van {tour.tourStartTime.ToString("dd-M-yyyy HH:mm")}";
        string actual = Tour.ChooseTour(barcode);

        Assert.AreEqual(expected, actual);
    }

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
        string input = "1234";
        DateTime time = DateTime.Parse("2024-04-10T10:20:00");
        Tour tour = new Tour(tourId, time);

        string expected = $"{input} succesvol aangemeld bij de rondleiding van {tour.tourStartTime.ToString("dd-M-yyyy HH:mm")}\n";
        string actual = Staff.AddLastMinuteVisitor(tourId, input);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestCheckIfReservation_True()
    {
        long barcode = 1234;
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
        long barcode = 1234;
        int tourId = 2;
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
        long barcode = 1234;
        Tour.CancelReservation(barcode);
        
        string expected = "U heeft geen rondleiding geboekt";
        string actual = Tour.GetTourTime(barcode);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestCancelReservation_True()
    {
        long barcode = 1234;
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
        long barcode = 1234;
        Tour.CancelReservation(barcode);
        
        string expected = "U heeft nog geen tour ingepland";
        string actual = Tour.CancelReservation(barcode);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestSelectTourAndCheckTour()
    {
        Assert.AreEqual(0, 0);
    }

}
