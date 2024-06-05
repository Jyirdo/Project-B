public class Presentation
{
    public static void WriteLine(string line)
    {
        Program.World.WriteLine(line);
    }

    public static string ReadLine()
    {
        return Program.World.ReadLine();
    }
}