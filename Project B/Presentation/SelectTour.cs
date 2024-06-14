namespace ProjectB;

public class SelectTour
{
    public static string SelectATour(string barcode)
    {
        Console.WriteLine("Bij deze rondleidingen kunt u zich op dit moment aanmelden:");
        Tour.Load_Tours(false);
        Console.WriteLine("\nToets het \x1b[34m\x1b[1mnummer\x1b[0m in van de tour waaraan u zich wilt aanmelden en druk op 'ENTER':");
        Console.WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk 'ENTER' om terug te gaan naar het hoofdmenu.");
        return Tour.ChooseTour(barcode);
    }
}