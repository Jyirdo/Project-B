namespace ProjectB;

public class Menu : View
{    
    public static string Show()
    {
        WriteLine($"\x1b[1m{Greeting.ShowGreeting()}scan de barcode op uw entreebewijs of medewerkerspas en druk op ENTER.\x1b[0m");
        WriteLine("Toets \x1b[33m'H'\x1b[0m en druk ENTER voor hulp.");
        return ReadLine();
    }
}