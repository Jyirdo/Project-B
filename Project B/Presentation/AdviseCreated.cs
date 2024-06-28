namespace ProjectB;

public class AdviseCreated : View
{
    public static string Show()
    {
        WriteLine("Het advies is succesvol aangemaakt en is terug te vinden onder 'Data/Advise.txt' \n\nDruk op ENTER om terug te gaan.");
        return ReadLine();
    }
}