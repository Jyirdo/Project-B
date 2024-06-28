namespace ProjectB;

public class StaffLogin : View
{
    public static string Show()
    {
        WriteLine("Voer het wachtwoord in of toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan.");
        return ReadLine();
    }
}