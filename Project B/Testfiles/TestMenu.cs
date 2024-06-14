namespace ProjectB;

public class TestMenu
{
    public readonly IWorld World;
    public TestMenu(IWorld world)
    {
        World = world;
    }

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
            // Console.Clear();
            World.WriteLine("Voer het wachtwoord in of toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan.");
            string password = "hetdepot2024!";
            string password_input = World.ReadLine();

            if (password_input == password)
            {
                // Console.Clear();
                StaffMenu();
            }
            else
            {
                // Console.Clear();
                MainMenu();
            }
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

    public void StaffMenu()
    {
        while (true)
        {
            World.WriteLine("\x1b[1mMEDEWERKERSMENU\x1b[0m");
            TestTour tour = new(World);
            tour.TestLoad_Tours();
            World.WriteLine("\x1b[35m\x1b[1mVoer de ID in van de rondleiding waarvan u de opties wilt zien en druk ENTER.\x1b[0m\n \nAndere opties:");
            World.WriteLine("Toets \x1b[33m'A'\x1b[0m en druk ENTER voor advies over rondleidingen.");
            World.WriteLine("Toets \x1b[31m'Z'\x1b[0m en druk ENTER om het programma af te sluiten.");
            World.WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan naar het hoofdmenu.");
            break;
        }
    }
}
