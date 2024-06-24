namespace ProjectB;

public class MissingFile : View
{
    public static void ShowVisitors()
    {
        WriteLine("Visitors.json ontbreekt. Graag assistentie zoeken.");
    }

    public static void ShowCSV()
    {
        WriteLine("parttakers.csv niet gevonden. Graag assistentie zoeken.");
    }
}