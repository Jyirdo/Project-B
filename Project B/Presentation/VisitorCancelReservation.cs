namespace ProjectB;

public class VisitorCancelReservation : View
{    
    public static string Show(string message)
    {
        WriteLine(message);
        WriteLine("Toets 'ENTER' om terug te gaan naar het hoofdmenu.");
        return ReadLine();
    }
}