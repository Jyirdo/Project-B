namespace ProjectB;

public class VisitorMakeReservation : View
{    
    public static string Show()
    {
        WriteLine("Bij deze rondleidingen kunt u zich op dit moment aanmelden:");
        WriteLine($"{string.Join("\n", Tour.ShowTours(false))}");
        WriteLine("\nToets het \x1b[34m\x1b[1mnummer\x1b[0m in van de tour waaraan u zich wilt aanmelden en druk op 'ENTER':");
        WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk 'ENTER' om terug te gaan naar het hoofdmenu.");
        return ReadLine();
    }
}