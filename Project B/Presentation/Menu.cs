public class Menu : Presentation
{
    public static void MainMenu()
    {
        while (true)
        {
            CreateJson.CheckTours();
            int currenthour = Convert.ToInt16(DateTime.Now.ToString("HH"));
            WriteLine($"\x1b[1m{Greeting.ShowGreeting(currenthour)}scan de barcode op uw entreebewijs of medewerkerspas en druk op ENTER.\x1b[0m");
            WriteLine("Toets \x1b[33m'H'\x1b[0m en druk ENTER voor hulp.");

            string input = ReadLine();
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
                Menu.StaffMenu();
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
                            WriteLine("U heeft een incorrecte invoer opgegeven, probeer het opnieuw.");
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
            WriteLine(Tour.GetTourTime(barcode, false));
            WriteLine("Toets \x1b[33m'A'\x1b[0m en druk ENTER om uw rondleiding te annuleren.");
            WriteLine("Toets \x1b[33m'H'\x1b[0m en druk ENTER voor hulp.");
            WriteLine("Toets \x1b[31m'Q'\x1b[0m en ENTER om terug te gaan naar het menu.");
            string input2 = ReadLine();
            switch (input2.ToLower())
            {
                case "a":
                    {
                        WriteLine(Tour.CancelReservation(barcode));
                        WriteLine("Toets 'ENTER' om terug te gaan naar het hoofdmenu.");
                        Console.ReadLine();
                        Console.Clear();
                        MainMenu();
                        break;
                    }
                case "h":
                    {

                        WriteLine("Er komt iemand aan om u te helpen, een ogenblik geduld alstublieft.");
                        WriteLine("Toets \x1b[31m'Q'\x1b[0m en ENTER om terug te gaan naar het menu.");
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
                        WriteLine("U heeft een incorrecte invoer opgegeven, probeer het opnieuw.\n");
                        continue;
                    }
            }
        }
    }

    public static void StaffMenu()
    {
        while (true)
        {
            WriteLine("\x1b[1mMEDEWERKERSMENU\x1b[0m");
            Tour.Load_Tours(true);
            WriteLine("\x1b[35m\x1b[1mVoer de ID in van de rondleiding waarvan u de opties wilt zien en druk ENTER.\x1b[0m\n \nAndere opties:");
            WriteLine("Toets \x1b[33m'A'\x1b[0m en druk ENTER voor advies over rondleidingen.");
            WriteLine("Toets \x1b[31m'Z'\x1b[0m en druk ENTER om het programma af te sluiten.");
            WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan naar het hoofdmenu.");

            string tourId = ReadLine();
            if (int.TryParse(tourId, out int tourIdInt))
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
                        WriteLine($"\x1b[35m\x1b[1mU heeft rondleiding {tourIdInt} geselecteerd.\x1b[0m");
                        WriteLine($"Toets \x1b[33m'L'\x1b[0m en druk ENTER om een bezoeker handmatig toe te voegen aan rondleiding \x1b[35m\x1b[1m{tourIdInt}\x1b[0m.");
                        WriteLine($"Toets \x1b[33m'C'\x1b[0m en druk ENTER om de bezoekers in rondleiding \x1b[35m\x1b[1m{tourIdInt}\x1b[0m te checken.");
                        WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan.");
                        string input = ReadLine();
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
                                    WriteLine($"Scan de barcode van de bezoeker die u handmatig wilt toevoegen aan rondleiding {tourIdInt}:");
                                    WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan.");
                                    string input1 = ReadLine();
                                    WriteLine(Staff.AddLastMinuteVisitor(tourIdInt, input1));
                                    break;
                                }
                            case "c":
                                {
                                    Staff.SelectTourAndCheckTour(tourIdInt);
                                    break;
                                }
                            default:
                                {
                                    WriteLine("U heeft een incorrecte invoer opgegeven, probeer het opnieuw.");
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
                            Advise.CreateAdvise();
                            break;
                        }
                    case "z":
                        {
                            Environment.Exit(0);
                            break;
                        }
                }
            }
        }
    }
}
