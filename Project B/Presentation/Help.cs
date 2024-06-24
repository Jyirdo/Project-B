namespace ProjectB;

public class Help : View
{
    public static void Show()
    {
        WriteLine("Er komt hulp aan, een ogenblik geduld alstublieft.");
        WriteLine("Toets \x1b[31m'Q'\x1b[0m druk op ENTER om terug te gaan.");
        ReadLine();
    }
}