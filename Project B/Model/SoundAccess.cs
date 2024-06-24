namespace ProjectB;
using System.Media;

public static class SoundAccess
{
    public static void PlayHelp()
    {
        if (OperatingSystem.IsWindows())
        {
            try
            {
                SoundPlayer soundPlayer = new("Data/Help.wav");
                soundPlayer.Play();
            }
            catch (Exception)
            {

            }
        }
    }

    public static void PlayAccepted()
    {
        if (OperatingSystem.IsWindows())
        {
            try
            {
                SoundPlayer soundPlayer = new("Data/Accepted.wav");
                soundPlayer.Play();
            }
            catch (Exception)
            {

            }
        }
    }

    public static void PlayDenied()
    {
        if (OperatingSystem.IsWindows())
        {
            try
            {
                SoundPlayer soundPlayer = new("Data/Denied.wav");
                soundPlayer.Play();
            }
            catch (Exception)
            {

            }
        }
    }
}



