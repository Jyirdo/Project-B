public class SelectTour
{
    public static string SelectATour(long barcode)
    {
        Tour.Load_Tours();
        Console.WriteLine("\nToets het nummer in van de tour waaraan u wilt deelnemen:");
        string input = Console.ReadLine();
        return Tour.ChooseTour(barcode, input);
    }
}