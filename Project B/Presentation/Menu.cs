public class Menu : Presentation
{
    public static void MainMenu()
    {
        while (true)
        {
            int currenthour = Convert.ToInt16(DateTime.Now.ToString("HH"));
            WriteLine($"{Greeting.ShowGreeting(currenthour)}scan de barcode op uw entreebewijs en druk op ENTER.");
            WriteLine("Toets \x1b[33m'H'\x1b[0m en druk ENTER voor hulp.");
            WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk ENTER om het programma af te sluiten.");

            string input = ReadLine();
            long.TryParse(input, out long barcode);
            if (Tour.CheckIfReservation(barcode) == true)
            {
                SubMenu(barcode);
            }
            else if (long.TryParse(input, out long barcode2))
            {
                WriteLine(SelectTour.SelectATour(barcode2));
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
                            break;
                        }
                    case "p":
                        {
                            StaffMenu();
                            break;
                        }
                    case "q":
                        {
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

    public static void SubMenu(long barcode)
    {
        while (true)
        {
            WriteLine(Tour.GetTourTime(barcode));
            WriteLine("Toets \x1b[33m'A'\x1b[0m en druk ENTER om uw rondleiding te annuleren.");
            WriteLine("Toets \x1b[33m'H'\x1b[0m en druk ENTER voor hulp.");
            WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk ENTER om het programma af te sluiten.");
            string input2 = ReadLine();
            switch (input2.ToLower())
            {
                case "a":
                    {
                        WriteLine(Tour.CancelReservation(barcode));
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
                        break;
                    }
                case "p":
                    {
                        StaffMenu();
                        break;
                    }
                case "q":
                    {
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
            WriteLine("Geef uw personeelscode op:");
            WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan.");
            string staffcode = ReadLine();

            if (staffcode.ToLower() == "q")
            {
                MainMenu();
            }
            else if (Staff.CorrectStaffCode(staffcode) == true)
            {
                Tour.Load_Tours();
                WriteLine("Voer de ID in van de tour die u wilt selecteren en druk ENTER.");
                WriteLine("Toets \x1b[33m'A'\x1b[0m en druk ENTER voor advies over rondleidingen.");
                WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan.");

                string tourId = ReadLine();
                if (int.TryParse(tourId, out int tourIdInt))
                {
                    while (true)
                    {
                        WriteLine("Toets \x1b[33m'L'\x1b[0m en druk ENTER om een bezoeker toe te voegen aan een rondleiding.");
                        WriteLine("Toets \x1b[33m'C'\x1b[0m en druk ENTER om de bezoekers in de tour te checken.");
                        WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan.");
                        string input = ReadLine();
                        switch (input.ToLower())
                        {
                            case "q":
                                {
                                    StaffMenu();
                                    break;
                                }
                            case "l":
                                {
                                    WriteLine("Scan de barcode van de bezoeker die u wilt toevoegen aan de rondleiding");
                                    WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan.");
                                    string input1 = ReadLine();
                                    WriteLine(Staff.AddLastMinuteVisitor(tourIdInt, input1));
                                    break;
                                }
                            case "c":
                                {
                                    WriteLine("Begin met barcodes scannen om te controleren of iedereen er is.");
                                    WriteLine("Toets \x1b[33m'K'\x1b[0m en druk ENTER wanneer u klaar bent.");
                                    WriteLine("Toets \x1b[31m'Q'\x1b[0m en druk ENTER om terug te gaan.");
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
                else
                {
                    switch (tourId)
                    {
                        case "q":
                            {
                                MainMenu();
                                break;
                            }
                        case "a":
                            {
                                //Advise.CreateAdvise();
                                break;
                            }
                    }
                }
            }
            else
            {
                WriteLine("Dat is geen correcte code.");
                continue;
            }
        }
    }
}