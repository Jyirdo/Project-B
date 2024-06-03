public class SelectTour : Presentation
{
    public static string SelectATour(long barcode)
    {
        Tour.Load_Tours();
        WriteLine("\nToets het nummer in van de tour waaraan u zich wilt aanmelden:");
        return Tour.ChooseTour(barcode);
    }
}