namespace ProjectB;

public class AllNotPresent : View
{
    public static void Show(string[] presenceListArray)
    {
        WriteLine("\x1b[1mDeze ID's zijn afwezig:\x1b[0m\n");
        WriteLine($"> {string.Join("\n> ", presenceListArray)}");
        WriteLine($"Aantal mensen afwezig: {presenceListArray.Length}.\n");
    }
}