namespace ProjectB;

class GuideGiveInfo : View
{
    public static void ShowOngeldigeTourId()
    {
        WriteLine("Dit is een ongeldig tourId.");
    }

    public static void ShowNoGuideInTour()
    {
        WriteLine("Deze gids is niet gekoppeld aan deze tour.");

    }

    public static void ShowAlreadyGuideInTour()
    {
        WriteLine("Er is al een gids die deze tour geeft.");
    }

    public static void ShowGuideAdded(string Name, int tourid)
    {
        WriteLine($"Gids {Name} successvol toegevoegd aan tour {tourid}.");
    }

    public static void ShowGuideRemoved(string Name, int tourid)
    {
        WriteLine($"Gids {Name} successvol verwijderd van tour {tourid}.");
    }
}