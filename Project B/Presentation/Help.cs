using System.Media;

public class Help
{
    public static void ShowHelp()
    {
        while (true)
        {
            Console.WriteLine("Er komt iemand aan om u te helpen, een ogenblik geduld alstublieft.");
            Console.WriteLine("Toets 'Q' en ENTER om terug te gaan naar het menu.");
            PlayJingle();
            string helpInput = Console.ReadLine().ToLower();
            if (helpInput == "q")
                Menu.ShowNotRegisteredMenu();
            else
            {
                Console.WriteLine("U heeft een incorrecte invoer opgegeven, probeer het opnieuw.");
                continue;
            }
        }
    
    }

    public static void PlayJingle()  // Make check for os, only works on windows.
    {
        using (SoundPlayer soundPlayer = new SoundPlayer("jingle.wav"))
            soundPlayer.Play();
    }
}