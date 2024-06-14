namespace ProjectB;

public class TestMenu
{
    public readonly IWorld World;

    public TestMenu(IWorld world)
    {
        World = world;
    }

    // public void MainMenu()
    // {
    //     string input = World.ReadLine();

    //     if (Visitor.HasTicket(input.Trim()))
    //     {
    //         //Console.Clear();
    //         World.WriteLine(TestSelectTour.SelectATour(input.Trim(), World));
    //         //Console.Clear();
    //     }
    // }

    public void MainMenu()
    {
        CreateJson.CheckTours();
        int currenthour = Convert.ToInt16(DateTime.Now.ToString("HH"));
        World.WriteLine($"\x1b[1m{Greeting.ShowGreeting(currenthour)}scan de barcode op uw entreebewijs of medewerkerspas en druk op ENTER.\x1b[0m");
        World.WriteLine("Toets \x1b[33m'H'\x1b[0m en druk ENTER voor hulp.");

        string input = World.ReadLine();
        if (TestTour.CheckIfReservation(input.Trim()) == "true")
        {
            SubMenu(input.Trim());
        }
        else if (TestTour.CheckIfReservation(input.Trim()) != "true" && TestTour.CheckIfReservation(input.Trim()) != "false")
        {
            World.WriteLine(TestTour.CheckIfReservation(input.Trim()));
        }
        else if (Staff.CorrectStaffCode(input.Trim()) == true)
        {
            //Console.Clear();
            // StaffMenu();
        }
        else if (Visitor.HasTicket(input.Trim()))
        {
            //Console.Clear();
            World.WriteLine(TestSelectTour.SelectATour(input.Trim(), World));
            // Console.Clear();
        }
    }

    public void SubMenu(string barcode)
    {
        
        World.WriteLine(Tour.GetTourTime(barcode, false));
        World.WriteLine("Toets \x1b[33m'A'\x1b[0m en druk ENTER om uw rondleiding te annuleren.");
        World.WriteLine("Toets \x1b[33m'H'\x1b[0m en druk ENTER voor hulp.");
        World.WriteLine("Toets \x1b[31m'Q'\x1b[0m en ENTER om terug te gaan naar het menu.");
        string input2 = World.ReadLine();
        switch (input2.ToLower())
        {
            case "a":
                {
                    World.WriteLine(TestTour.CancelReservation(barcode));
                    //MainMenu();
                    break;
                }
        }
    }
}
