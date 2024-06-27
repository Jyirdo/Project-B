namespace ProjectB;

public class StaffPresenceCheckedTourFull : View
{
    public static string Show()
    {
        WriteLine("Deze tour is vol en er kunnen geen nieuwe deelnemers meer aan worden toegevoegd.");
        WriteLine("Druk op \x1b[32m'S'\x1b[0m om de tour te starten.");
        WriteLine("Druk op \x1b[31m'Q'\x1b[0m om het starten van de tour te annuleren.");
        return ReadLine();
    }
}