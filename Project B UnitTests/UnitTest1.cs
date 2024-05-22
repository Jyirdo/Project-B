namespace Project_B_UnitTests;
using NUnit.Framework;
using System;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
    }
    public void VisitorTest()
    {
        long expectedBarcode = 1234567890;
        DateTime expectedTourTime = new DateTime(2024, 5, 22, 14, 30, 0);
        int expectedTourNumber = 1;

        Visitor visitor = new Visitor(expectedBarcode, expectedTourTime, expectedTourNumber);

        Assert.AreEqual(expectedBarcode, visitor.barcode);
        Assert.AreEqual(expectedTourTime, visitor.tourTime);
        Assert.AreEqual(expectedTourNumber, visitor.tourNumber);
    }
}
