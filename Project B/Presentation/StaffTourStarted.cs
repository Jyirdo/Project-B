namespace ProjectB;

public class StaffTourStarted : View
{
    public static string Show()
    {
        WriteLine("\x1b[32;1mDe rondleiding is succesvol gestart.\x1b[0m");
        WriteLine("\nToets 'ENTER' om terug te gaan naar het hoofdmenu.");
        return ReadLine();
    }
}