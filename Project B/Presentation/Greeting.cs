public class Greeting
{
    public static string ShowGreeting(int currentHour)
    {
        // Find out what time it is and greet the user appropriatly
        if (currentHour < 6)
        {
            return "Goedennacht, ";
        }
        else if (currentHour < 12)
        {
            return "Goedemorgen, ";
        }
        else if (currentHour < 18)
        {
            return "Goedemiddag, ";
        }
        else if (currentHour < 24)
        {
            return "Goedenavond, ";
        }
        return null;
    }
}
