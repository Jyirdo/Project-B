namespace ProjectB;

public class StaffCheckPresenceSucces : View
{
    public static void Show(string checkPresence)
    {
        WriteLine($"\x1b[32;1m{checkPresence} is gecontroleerd.\x1b[0m");
    }
}