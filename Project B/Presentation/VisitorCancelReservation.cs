namespace ProjectB;

public class VisitorCancelReservation : View
{    
    public static string Show(DateTime tourStartTime)
    {
        WriteLine($"Uw reservering voor de rondleiding van \x1b[32m{tourStartTime.ToString("dd-M-yyyy HH:mm")}\x1b[0m is geannuleerd. Nog een prettige dag verder!\n");
        WriteLine("Toets 'ENTER' om terug te gaan naar het hoofdmenu.");
        return ReadLine();
    }
}