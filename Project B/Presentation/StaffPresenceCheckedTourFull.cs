namespace ProjectB;

public class StaffPresenceCheckedTourFull : View
{
    public static string Show()
    {
        WriteLine("Druk op \x1b[32m'S'\x1b[0m om de tour te starten.");
        WriteLine("Druk op \x1b[31m'Q'\x1b[0m om terug te gaan en verder te scannen.");
        return ReadLine();
    }
}