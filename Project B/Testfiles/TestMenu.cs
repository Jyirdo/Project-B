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
    //         World.WriteLine("kek");
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
            //Console.Clear();
            // SubMenu(input.Trim());
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
            Console.WriteLine("Toets 'ENTER' om terug te gaan naar het hoofdmenu.");
            Console.ReadLine();
            // Console.Clear();
        }
        else
        {
            switch (input.ToLower().Trim())
            {
                case "h":
                    {
                        Help helpMenu = new();
                        //Console.Clear();
                        World.WriteLine("Er komt hulp aan, een ogenblik geduld.");
                        World.WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan.");
                        helpMenu.ShowHelp(null);
                        break;
                    }
            }
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
                    // World.WriteLine("Toets 'ENTER' om terug te gaan naar het hoofdmenu.");
                    // Console.ReadLine();
                    // Console.Clear();
                    //MainMenu();
                    break;
                }
            case "h":
                {

                    World.WriteLine("Er komt iemand aan om u te helpen, een ogenblik geduld alstublieft.");
                    World.WriteLine("Toets \x1b[31m'Q'\x1b[0m en ENTER om terug te gaan naar het menu.");
                    Help helpMenu = new();
                Start:
                    string message = helpMenu.ShowHelp(null);
                    if (message != null)
                        goto Start;
                    //Console.Clear();
                    break;
                }
            case "q":
                {
                    //Console.Clear();
                    MainMenu();
                    break;
                }
            // default:
            //     {
            //         World.WriteLine("U heeft een incorrecte invoer opgegeven, probeer het opnieuw.\n");
            //         continue;
            //     }
        }
    }

    // public void StaffMenu()
    // {
    //     while (true)
    //     {
    //         World.WriteLine("\x1b[1mMEDEWERKERSMENU\x1b[0m");
    //         Tour.Load_Tours(true);
    //         World.WriteLine("\x1b[35m\x1b[1mVoer de ID in van de rondleiding waarvan u de opties wilt zien en druk ENTER.\x1b[0m\n \nAndere opties:");
    //         World.WriteLine("Toets \x1b[33m'A'\x1b[0m en druk ENTER voor advies over rondleidingen.");
    //         World.WriteLine("Toets \x1b[31m'Z'\x1b[0m en druk ENTER om het programma af te sluiten.");
    //         World.WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan naar het hoofdmenu.");

    //         string tourId = World.ReadLine();
    //         if (int.TryParse(tourId, out int tourIdInt) && tourIdInt > 0 && tourIdInt <= BaseAccess.LoadAll().Count())
    //         {
    //             if (Tour.CheckIfTourIsStarted(tourIdInt) != null)
    //             {
    //                 Console.WriteLine(Tour.CheckIfTourIsStarted(tourIdInt));
    //                 StaffMenu();
    //             }
    //             else
    //             {
    //                 while (true)
    //                 {
    //                     //Console.Clear();
    //                     World.WriteLine($"\x1b[35m\x1b[1mU heeft rondleiding {tourIdInt} geselecteerd.\x1b[0m");
    //                     World.WriteLine($"Toets \x1b[33m'L'\x1b[0m en druk ENTER om een bezoeker handmatig toe te voegen aan rondleiding \x1b[35m\x1b[1m{tourIdInt}\x1b[0m.");
    //                     World.WriteLine($"Toets \x1b[33m'C'\x1b[0m en druk ENTER om de bezoekers in rondleiding \x1b[35m\x1b[1m{tourIdInt}\x1b[0m te checken.");
    //                     World.WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan.");
    //                     string input = World.ReadLine();
    //                     //Console.Clear();
    //                     switch (input.ToLower())
    //                     {
    //                         case "q":
    //                             {
    //                                 //Console.Clear();
    //                                 StaffMenu();
    //                                 break;
    //                             }
    //                         case "l":
    //                             {
    //                                 World.WriteLine($"Scan de barcode van de bezoeker die u handmatig wilt toevoegen aan rondleiding {tourIdInt}:");
    //                                 World.WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan.");
    //                                 string input1 = World.ReadLine();
    //                                 World.WriteLine(Staff.AddLastMinuteVisitor(tourIdInt, input1));
    //                                 break;
    //                             }
    //                         case "c":
    //                             {
    //                                 Staff.SelectTourAndCheckTour(tourIdInt);
    //                                 break;
    //                             }
    //                         default:
    //                             {
    //                                 World.WriteLine("U heeft een incorrecte invoer opgegeven, probeer het opnieuw.");
    //                                 continue;
    //                             }
    //                     }
    //                 }
    //             }
    //         }
    //         else
    //         {
    //             switch (tourId)
    //             {
    //                 case "q":
    //                     {
    //                         //Console.Clear();
    //                         MainMenu();
    //                         break;
    //                     }
    //                 case "a":
    //                     {
    //                         Advise.CreateAdvise();
    //                         break;
    //                     }
    //                 case "z":
    //                     {
    //                         Environment.Exit(0);
    //                         break;
    //                     }
    //                 default:
    //                     {
    //                         //Console.Clear();
    //                         World.WriteLine("U heeft een incorrecte invoer opgegeven, probeer het opnieuw.");
    //                         continue;
    //                     }
    //             }
    //         }
    //     }
    // }
}
