using System.Media;

public class Help : IInput
{

    public string GetInput()
    {
        return Console.ReadLine().ToLower().Trim();
    }

    public static void PlayJingle()
    {
        if (OperatingSystem.IsWindows())
        {
            using (SoundPlayer soundPlayer = new SoundPlayer())
                soundPlayer.Play();
        }
    }

    public string ShowHelp(string input) // Input null for normal function and string for testing.
    {
        PlayJingle();
        if (input == null)
        {
            if (GetInput() == "q")
                return null;
        }
        else if (input is string)
        {
            if (input == "q")
                return null;
        }

        return "U heeft een incorrecte invoer opgegeven, probeer het opnieuw.";
    }
}



