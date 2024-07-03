namespace ProjectB;

public class VisitorMakeReservationSucces : View
{
    public static string Show(DateTime tourStartTime)
    {
        WriteLine($"Succesvol gereserveerd voor de rondleiding van \x1b[32m{tourStartTime.ToString("dd-M-yyyy HH:mm")}\x1b[0m\n");
        WriteLine("Toets 'ENTER' om terug te gaan naar het hoofdmenu.");
        return ReadLine();
    }
}