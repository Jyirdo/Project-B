namespace ProjectB;

public class StaffTourSelected : View
{
    public static string Show(int tourID)
    {
        WriteLine($"\x1b[35m\x1b[1mU heeft rondleiding {tourID} geselecteerd.\x1b[0m");
        WriteLine($"Toets \x1b[33m'C'\x1b[0m en druk ENTER om de aanwezige bezoekers bij rondleiding \x1b[35m\x1b[1m{tourID}\x1b[0m aan te melden.");
        WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan.");
        return ReadLine();
    }
}