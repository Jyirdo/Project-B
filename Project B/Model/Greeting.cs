namespace ProjectB;

class Greeting
{
    public static string ShowGreeting()
    {
        int CurrentHour = Program.World.Now.Hour;
        // Find out what time it is and greet the user appropriatly
        if (CurrentHour < 6 && CurrentHour >= 0)
        {
            return "Goedennacht, ";
        }
        else if (CurrentHour < 12 && CurrentHour >= 0)
        {
            return "Goedemorgen, ";
        }
        else if (CurrentHour < 18 && CurrentHour >= 0)
        {
            return "Goedemiddag, ";
        }
        else if (CurrentHour < 24 && CurrentHour >= 0)
        {
            return "Goedenavond, ";
        }
        else if (CurrentHour == 24 && CurrentHour >= 0)
        {
            return "Goedennacht, ";
        }
        else
        {
            return "Welkom, ";
        }
    }
}
