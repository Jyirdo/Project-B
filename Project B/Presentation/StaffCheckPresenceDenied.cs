namespace ProjectB;

public class StaffCheckPresenceDenied : View
{
    public static void Show(string checkPresence, string tourTime)
    {
        WriteLine($"\x1b[33;1mDeze bezoeker ({checkPresence}) heeft zich aangemeld bij een \x1b[33;1;4mandere rondleiding\x1b[0m\x1b[33;1m (van {tourTime})\x1b[0m");
        WriteLine("\x1b[31;1mDus dat is een incorrecte barcode.\x1b[0m\n");
    }
}