namespace Project_B_UnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
<<<<<<< HEAD
        int barcode = 1234567890;
        Tour tour = new Tour(barcode, DateTime.Now);

        string expected = $"Succesvol aangemeld bij de rondleiding van {tour.tourStartTime.ToString("dd-M-yyyy HH:mm")}";
        string actual = Tour.Choose_Tour(barcode);

        Assert.AreEqual(expected, actual);
=======
        Assert.AreEqual(0, 0);
>>>>>>> main
    }

    [TestMethod]
    public void TestMethod4()
    {
        Assert.AreEqual(0, 0);
    }

    [TestMethod]
    public void TestMethod5()
    {
        Assert.AreEqual(0, 0);
    }

    [TestMethod]
    public void TestMethod6()
    {
        Assert.AreEqual(0, 0);
    }

    [TestMethod]
    public void TestMethod7()
    {
        Assert.AreEqual(0, 0);
    }

    [TestMethod]
    public void TestMethod8()
    {
        Assert.AreEqual(0, 0);
    }

    [TestMethod]
    public void TestMethod9()
    {
        Assert.AreEqual(0, 0);
    }

    [TestMethod]
    public void TestMethod10()
    {
        Assert.AreEqual(0, 0);
    }

    [TestMethod]
    public void TestMethod11()
    {
        Assert.AreEqual(0, 0);
    }

    [TestMethod]
    public void TestMethod12()
    {
        Assert.AreEqual(0, 0);
    }

    [TestMethod]
    public void TestMethod13()
    {
        Assert.AreEqual(0, 0);
    }

}
