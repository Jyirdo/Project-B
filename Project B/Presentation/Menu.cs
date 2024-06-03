public class Menu
{
    public static void MainMenu()
    {
        while (true)
        {
            int currenthour = Convert.ToInt16(DateTime.Now.ToString("HH"));
            Console.Write(Greeting.ShowGreeting(currenthour));
            Console.WriteLine("scan de barcode op uw entreebewijs en druk op ENTER.");
            Console.WriteLine("Toets \x1b[33m'H'\x1b[0m en druk ENTER voor hulp.");

            string input = Console.ReadLine();
            long.TryParse(input, out long barcode);
            if (Tour.CheckIfReservation(barcode) == true)
            {   
                Console.Clear();
                SubMenu(barcode);
            }
            else if (long.TryParse(input, out long barcode2))
            {
                Console.Clear();
                Console.WriteLine(SelectTour.SelectATour(barcode2));
                Console.WriteLine("Toets 'ENTER' om terug te gaan naar het hoofdmenu.");
                Console.ReadLine();
                Console.Clear();
                continue;
            }
            else
            {
                switch (input.ToLower())
                {
                    case "h":
                        {
                            Help helpMenu = new();
                            helpMenu.ShowHelp(null);
                            Console.Clear();
                            break;
                        }
                    case "p":
                        {
                            Console.Clear();
                            StaffMenu();
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

    public static void SubMenu(long barcode)
    {
        while (true)
        {
            Console.WriteLine(Tour.GetTourTime(barcode));
            Console.WriteLine("Toets \x1b[33m'A'\x1b[0m en druk ENTER om uw rondleiding te annuleren.");
            Console.WriteLine("Toets \x1b[33m'H'\x1b[0m en druk ENTER voor hulp.");
            Console.WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan naar het hoofdmenu.");
            string input2 = Console.ReadLine();
            switch (input2.ToLower())
            {
                case "a":
                    {
                        Console.WriteLine(Tour.CancelReservation(barcode));
                        Console.WriteLine("Toets 'ENTER' om terug te gaan naar het hoofdmenu.");
                        Console.ReadLine();
                        Console.Clear();
                        MainMenu();
                        break;
                    }
                case "h":
                    {

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
            Console.WriteLine("Geef uw personeelscode op:"); 
            Console.WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan.");
            string staffcode = Console.ReadLine();

            if (staffcode.ToLower() == "q")
            {
                Console.Clear();
                MainMenu();
            }
            else if (Staff.CorrectStaffCode(staffcode) == true)
            {
                Console.Clear();
                Tour.Load_Tours();
                Console.WriteLine("Voer de ID in van de tour die u wilt selecteren en druk ENTER.");
                Console.WriteLine("Toets \x1b[33m'A'\x1b[0m en druk ENTER voor advies over rondleidingen.");
                Console.WriteLine("Toets \x1b[31m'Z'\x1b[0m en druk ENTER om het programma af te sluiten.");
                Console.WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan.");

                string tourId = Console.ReadLine();
                if (int.TryParse(tourId, out int tourIdInt))
                {
                    while (true)
                    {
                        Console.WriteLine("Toets \x1b[33m'L'\x1b[0m en druk ENTER om een bezoeker toe te voegen aan een rondleiding.");
                        Console.WriteLine("Toets \x1b[33m'C'\x1b[0m en druk ENTER om de bezoekers in de tour te checken.");
                        Console.WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan.");
                        string input = Console.ReadLine();
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
                                    Console.WriteLine("Scan de barcode van de bezoeker die u wilt toevoegen aan de rondleiding");
                                    Console.WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan.");
                                    string input1 = Console.ReadLine();
                                    Console.WriteLine(Staff.AddLastMinuteVisitor(tourIdInt, input1));
                                    break;
                                }
                            case "c":
                                {
                                    Console.WriteLine("Begin met barcodes scannen om te controleren of iedereen er is.");
                                    Console.WriteLine("Toets \x1b[33m'K'\x1b[0m en druk ENTER wanneer u klaar bent.");
                                    Console.WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan.");
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
                                //Advise.CreateAdvise();
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
            else
            {
                Console.WriteLine("Dat is geen correcte code.");
                continue;
            }
        }
    }
}