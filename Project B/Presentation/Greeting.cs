namespace ProjectB;

public class Greeting
{
    public static string ShowGreeting(int currentHour)
    {
        // Find out what time it is and greet the user appropriatly
        if (currentHour < 6 && currentHour >= 0)
        {
            return "Goedennacht, ";
        }
        else if (currentHour < 12 && currentHour >= 0)
        {
            return "Goedemorgen, ";
        }
        else if (currentHour < 18 && currentHour >= 0)
        {
            return "Goedemiddag, ";
        }
        else if (currentHour < 24 && currentHour >= 0)
        {
            return "Goedenavond, ";
        }
        else if (currentHour == 24 && currentHour >= 0)
        {
            return "Goedennacht, ";
        }
        else
        {
            return "Welkom, ";
        }
    }
}
