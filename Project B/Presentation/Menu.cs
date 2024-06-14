namespace ProjectB;

public class Menu
{
    public readonly IWorld World;
    public Menu(IWorld world)
    {
        World = world;
    }

    public static void MainMenu()
    {
        while (true)
        {
            CreateJson.CheckTours();
            int currenthour = Convert.ToInt16(DateTime.Now.ToString("HH"));
            Console.WriteLine($"\x1b[1m{Greeting.ShowGreeting(currenthour)}scan de barcode op uw entreebewijs of medewerkerspas en druk op ENTER.\x1b[0m");
            Console.WriteLine("Toets \x1b[33m'H'\x1b[0m en druk ENTER voor hulp.");

            string input = Console.ReadLine();
            if (Tour.CheckIfReservation(input.Trim()) == "true")
            {
                Console.Clear();
                SubMenu(input.Trim());
            }
            else if (Tour.CheckIfReservation(input.Trim()) != "true" && Tour.CheckIfReservation(input.Trim()) != "false")
            {
                Console.WriteLine(Tour.CheckIfReservation(input.Trim()));
                continue;
            }
            else if (Staff.CorrectStaffCode(input.Trim()) == true)
            {
                Console.Clear();
                Console.WriteLine("Voer het wachtwoord in of toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan.");
                string password = "hetdepot2024!";
                string password_input = Console.ReadLine();

                if (password_input == password)
                {
                    Console.Clear();
                    StaffMenu();
                }
                else
                {
                    Console.Clear();
                    MainMenu();
                }
            }
            else if (Visitor.HasTicket(input.Trim()))
            {
                Console.Clear();
                Console.WriteLine(SelectTour.SelectATour(input.Trim()));
                Console.WriteLine("Toets 'ENTER' om terug te gaan naar het hoofdmenu.");
                Console.ReadLine();
                Console.Clear();
                continue;
            }
            else
            {
                switch (input.ToLower().Trim())
                {
                    case "h":
                        {
                            Help helpMenu = new();
                            Console.Clear();
                            Console.WriteLine("Er komt hulp aan, een ogenblik geduld.");
                            Console.WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan.");
                            helpMenu.ShowHelp(null);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("U heeft een incorrecte invoer opgegeven, probeer het opnieuw.");
                            continue;
                        }
                }
            }
        }
    }

    public static void SubMenu(string barcode)
    {
        while (true)
        {
            Console.WriteLine(Tour.GetTourTime(barcode, false));
            Console.WriteLine("Toets \x1b[33m'A'\x1b[0m en druk ENTER om uw rondleiding te annuleren.");
            Console.WriteLine("Toets \x1b[33m'H'\x1b[0m en druk ENTER voor hulp.");
            Console.WriteLine("Toets \x1b[31m'Q'\x1b[0m en ENTER om terug te gaan naar het hoofdmenu.");
            string input2 = Console.ReadLine();
            switch (input2.ToLower())
            {
                case "a":
                    {
                        Console.WriteLine(Tour.CancelReservation(barcode));
                        Console.WriteLine("\nToets 'ENTER' om terug te gaan naar het hoofdmenu.");
                        Console.ReadLine();
                        Console.Clear();
                        MainMenu();
                        break;
                    }
                case "h":
                    {
                        Console.Clear();
                        Console.WriteLine("Er komt iemand aan om u te helpen, een ogenblik geduld alstublieft.");
                        Console.WriteLine("Toets \x1b[31m'Q'\x1b[0m en ENTER om terug te gaan naar het menu.");
                        Help helpMenu = new();
                    Start:
                        string message = helpMenu.ShowHelp(null);
                        if (message != null)
                            goto Start;
                        Console.Clear();
                        break;
                    }
                case "p":
                    {
                        Console.Clear();
                        StaffMenu();
                        break;
                    }
                case "q":
                    {
                        Console.Clear();
                        MainMenu();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("U heeft een incorrecte invoer opgegeven, probeer het opnieuw.\n");
                        continue;
                    }
            }
        }
    }

    public static void StaffMenu()
    {
        while (true)
        {
            Console.WriteLine("\x1b[1mMEDEWERKERSMENU\x1b[0m");
            Tour.Load_Tours(true);
            Console.WriteLine("\x1b[35m\x1b[1mVoer de ID in van de rondleiding waarvan u de opties wilt zien en druk ENTER.\x1b[0m\n \nAndere opties:");
            Console.WriteLine("Toets \x1b[33m'A'\x1b[0m en druk ENTER voor advies over rondleidingen.");
            Console.WriteLine("Toets \x1b[31m'Z'\x1b[0m en druk ENTER om het programma af te sluiten.");
            Console.WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan naar het hoofdmenu.");

            string tourId = Console.ReadLine();
            if (int.TryParse(tourId, out int tourIdInt) && tourIdInt > 0 && tourIdInt <= BaseAccess.LoadAll().Count())
            {
                if (Tour.CheckIfTourIsStarted(tourIdInt) != null)
                {
                    Console.WriteLine(Tour.CheckIfTourIsStarted(tourIdInt));
                    StaffMenu();
                }
                else
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine($"\x1b[35m\x1b[1mU heeft rondleiding {tourIdInt} geselecteerd.\x1b[0m");
                        Console.WriteLine($"Toets \x1b[33m'L'\x1b[0m en druk ENTER om een bezoeker handmatig toe te voegen aan rondleiding \x1b[35m\x1b[1m{tourIdInt}\x1b[0m.");
                        Console.WriteLine($"Toets \x1b[33m'C'\x1b[0m en druk ENTER om de bezoekers in rondleiding \x1b[35m\x1b[1m{tourIdInt}\x1b[0m te checken.");
                        Console.WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan.");
                        string input = Console.ReadLine();
                        Console.Clear();
                        switch (input.ToLower())
                        {
                            case "q":
                                {
                                    Console.Clear();
                                    StaffMenu();
                                    break;
                                }
                            case "l":
                                {
                                    Console.WriteLine($"Scan de barcode van de bezoeker die u handmatig wilt toevoegen aan rondleiding {tourIdInt}:");
                                    Console.WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan.");
                                    string input1 = Console.ReadLine();
                                    Console.WriteLine(Staff.AddLastMinuteVisitor(tourIdInt, input1));
                                    break;
                                }
                            case "c":
                                {
                                    Staff.SelectTourAndCheckTour(tourIdInt);
                                    break;
                                }
                            default:
                                {
                                    Console.WriteLine("U heeft een incorrecte invoer opgegeven, probeer het opnieuw.");
                                    continue;
                                }
                        }
                    }
                }
            }
            else
            {
                switch (tourId)
                {
                    case "q":
                        {
                            Console.Clear();
                            MainMenu();
                            break;
                        }
                    case "a":
                        {
                            Console.Clear();
                            Advise.CreateAdvise();
                            break;
                        }
                    case "z":
                        {
                            Environment.Exit(0);
                            break;
                        }
                    default:
                        {
                            Console.Clear();
                            Console.WriteLine("U heeft een incorrecte invoer opgegeven, probeer het opnieuw.");
                            continue;
                        }
                }
            }
        }
    }
}