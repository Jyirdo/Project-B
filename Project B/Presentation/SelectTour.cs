public class SelectTour : Presentation
{
    public static string SelectATour(long barcode)
    {
        Tour.Load_Tours();
        WriteLine("\nToets het \x1b[34m\x1b[1mnummer\x1b[0m in van de tour waaraan u zich wilt aanmelden:");
        WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan naar het hoofdmenu.");
        return Tour.ChooseTour(barcode);
    }
}