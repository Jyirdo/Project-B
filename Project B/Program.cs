namespace ProjectB;

public static class Program
{
    public static IWorld World = new RealWorld();

    public static void Main()
    {
        // MenuController.Start();
        StaffController.SelectionMenu();
    }
}