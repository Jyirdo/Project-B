namespace ProjectB;

public class StaffMenu : View
{
    public static string Show()
    {
        WriteLine("\x1b[1mMEDEWERKERSMENU\x1b[0m");
        WriteLine($"{string.Join("\n", Tour.ShowTours(true))}");
        WriteLine("\x1b[35m\x1b[1mVoer de ID in van de rondleiding waarvan u de opties wilt zien en druk ENTER.\x1b[0m\n \nAndere opties:");
        WriteLine("Toets \x1b[33m'A'\x1b[0m en druk ENTER voor advies over rondleidingen.");
        WriteLine("Toets \x1b[31m'Z'\x1b[0m en druk ENTER om het programma af te sluiten.");
        WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan naar het hoofdmenu.");
        return ReadLine();
    }
}