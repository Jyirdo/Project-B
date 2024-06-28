namespace ProjectB;

public class VisitorMenu : View
{
    public static string Show(Visitor visitor)
    {
        WriteLine(Tour.GetTourTime(visitor, false));
        WriteLine("Toets \x1b[33m'A'\x1b[0m en druk ENTER om uw rondleiding te annuleren.");
        WriteLine("Toets \x1b[33m'H'\x1b[0m en druk ENTER voor hulp.");
        WriteLine("Toets \x1b[31m'Q'\x1b[0m en ENTER om terug te gaan naar het hoofdmenu.");
        return ReadLine();
    }
}