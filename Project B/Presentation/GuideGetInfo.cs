namespace ProjectB;

class GuideGetInfo : View
{
    public static string ShowTourIdAdd()
    {
        WriteLine("Voer het ID in van de tour waar u een gids aan toe wilt voegen.");
        return ReadLine();
    }
    public static string ShowTourIdRemove()
    {
        WriteLine("Voer het ID in van de tour waar u een gids uit wilt verwijderen.");
        return ReadLine();
    }
    public static string ShowGuideCode()
    {
        WriteLine("Scan de code van de gids.");
        return ReadLine();
    }
}