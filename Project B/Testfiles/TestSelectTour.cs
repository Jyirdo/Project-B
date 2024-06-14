namespace ProjectB;

public class TestSelectTour
{
    public static string SelectATour(string barcode, IWorld World)
    {
        World.WriteLine("Bij deze rondleidingen kunt u zich op dit moment aanmelden:");
        TestTour.Load_Tours(false);
        World.WriteLine("\nToets het \x1b[34m\x1b[1mnummer\x1b[0m in van de tour waaraan u zich wilt aanmelden:");
        World.WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan naar het hoofdmenu.");
        TestTour tour = new(World);
        return tour.ChooseTour(barcode);
    }
}