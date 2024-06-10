namespace ProjectB;

public static class Program
{
    public static void Main()
    {
        RealWorld world = new();
        Menu menu = new(world);
        menu.MainMenu();
    }
}