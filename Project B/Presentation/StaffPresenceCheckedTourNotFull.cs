namespace ProjectB;

public class StaffPresenceCheckedTourNotFull : View
{
    public static string Show(int parttakers, int limit)
    {
        WriteLine($"Er zijn nog {limit - parttakers} plaatsen over. Barcodes die nu gescand worden kunnen nog deelnemen aan de rondleiding.\n");
        WriteLine("Druk op \x1b[32m'S'\x1b[0m om de tour te starten.");
        WriteLine("Druk op \x1b[31m'Q'\x1b[0m om terug te gaan en verder te scannen.");
        return ReadLine();
    }
}